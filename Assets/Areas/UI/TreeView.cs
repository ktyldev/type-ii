using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeView : MonoBehaviour
{
    public GameObject treeViewItem;

    // private TreeViewItem _root;
    private SolarSystem _solarSystem;
    private Dictionary<OrbitalBody, RectTransform> _nodes;

    // Use this for initialization
    void Start()
    {
        _nodes = new Dictionary<OrbitalBody, RectTransform>();
        _solarSystem = GameObject.FindGameObjectWithTag(GameTags.SolarSystem).GetComponent<SolarSystem>();
        AddNode(GetComponent<RectTransform>(), _solarSystem.tree.root);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddNode(RectTransform parent, OrbitalBody body)
    {
        var node = Instantiate(treeViewItem, parent);
        _nodes.Add(body, node.GetComponent<RectTransform>());

        // adjust position relative to parent

        // children
        _solarSystem.tree
            .GetChildren(body)
            .ForEach(b => AddNode(node.GetComponent<RectTransform>(), b));
    }
}
