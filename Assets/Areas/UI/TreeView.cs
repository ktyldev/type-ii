using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeView : RowsView
{
    public GameObject treeViewItem;
    public int indentAmount;
    
    private RecursiveTree<OrbitalBody> _tree;
    private Dictionary<TreeViewItem, OrbitalBody> _nodes;
    private TreeViewItem _rootNode;
    
    void Start()
    {
        _nodes = new Dictionary<TreeViewItem, OrbitalBody>();
        _tree = GameObject.FindGameObjectWithTag(GameTags.SolarSystem).GetComponent<SolarSystem>().tree;

        _rootNode = BuildNode(_tree.root);
    }

    private TreeViewItem BuildNode(OrbitalBody body)
    {
        var node = Instantiate(treeViewItem, transform).GetComponent<TreeViewItem>();
        node.Text = body.designation;
        node.transform.Translate(indentAmount * _tree.GetDepth(body), 0, 0);

        AddRow(node.gameObject);
        _nodes.Add(node, body);

        node.OnExpand.AddListener(() =>
        {
            _tree
                .GetChildren(body)
                .ForEach(c => BuildNode(c));
        });

        node.Expand();
        Invalidate();
        return node;
    }

    protected override GameObject[] OrderRows(GameObject[] rows)
    {
        var groups = rows.Select(r => _nodes[r.GetComponent<TreeViewItem>()])
            .OrderBy(b => b.distanceFromParent)
            .GroupBy(_tree.GetParent)
            .ToList();

        var results = new List<OrbitalBody>();

        var rootGroup = groups.Where(g => g.Key == null).Single();
        results.Add(rootGroup.Single());
        groups.Remove(rootGroup);
        
        while (groups.Any())
        {
            var unpopulatedParent = results
                .Where(b => _tree.GetChildren(b).Any() && groups.Select(g => g.Key).Contains(b))
                .First();

            var groupToAdd = groups.Single(g => g.Key == unpopulatedParent);

            for (int i = 0; i < groupToAdd.Count(); i++)
            {
                var parentIndex = results.IndexOf(groupToAdd.Key);
                var insertIndex = parentIndex + i + 1;
                results.Insert(insertIndex, groupToAdd.ToArray()[i]);
            }
            groups.Remove(groupToAdd);
        }
 
        return results
            .Select(b => _nodes.Single(n => n.Value == b).Key.gameObject)
            .ToArray();
    }
}
