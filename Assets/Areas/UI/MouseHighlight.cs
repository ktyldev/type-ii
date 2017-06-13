using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseHighlight : MonoBehaviour {

    private List<KeplerTreeNode> _allNodes;

    // Use this for initialization
    void Start() {
        _allNodes = SolarSystem.Instance.tree.GetAll();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonUp(1)) {
            var closest = _allNodes
                .Select(n => new {
                    Data = n,
                    Distance = GetDistance(n)
                })
                .OrderBy(_ => _.Distance)
                .FirstOrDefault();

            Debug.Log(closest.Data.designation + ": " + closest.Distance.ToString());
        }
    }

    private float GetDistance(KeplerTreeNode node) {
        var nodeScreenPos = Camera.main.WorldToScreenPoint(node.transform.position);
        return Vector3.Distance(Input.mousePosition, nodeScreenPos);
    }
}
