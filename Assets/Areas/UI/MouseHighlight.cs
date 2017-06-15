using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseHighlight : MonoBehaviour {

    public GameObject gameController;
    public GameObject highlightGraphic;

    private MouseManager _manager;
    private List<OrbitalBody> _allNodes;

    private Transform _target;
    private GameObject _graphic;

    // Use this for initialization
    void Start() {
        _allNodes = SolarSystem.Instance.tree.GetAll();
        _manager = gameController.GetComponent<MouseManager>();
        _manager.onSelect.AddListener(() => BeginHighlighting(_manager.selectedObject));
        _manager.onDeselect.AddListener(StopHighlighting);
    }

    // Update is called once per frame
    void Update() {
        if (_target == null)
            return;

        _graphic.transform.position = Camera.main.WorldToScreenPoint(_target.position);
    }

    private void BeginHighlighting(GameObject obj) {
        _target = obj.transform;
        _graphic = Instantiate(highlightGraphic, transform);
    }

    private void StopHighlighting() {
        Destroy(_graphic);
        _target = null;
    }

    private float GetDistance(OrbitalBody node) {
        var nodeScreenPos = Camera.main.WorldToScreenPoint(node.transform.position);
        return Vector3.Distance(Input.mousePosition, nodeScreenPos);
    }
}
