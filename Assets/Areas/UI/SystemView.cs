using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TypeII;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class SystemView : MonoBehaviour {

    public GameObject gameController;

    private RecursiveTree<KeplerTreeNode> _tree;
    private Rect _panelRect;
    private CameraController _camera;
    private MouseManager _mouse;

    void Awake() {
        _panelRect = new Rect(10, 10, 200, Screen.height - 10);
    }

    // Use this for initialization
    void Start() {
        _tree = SolarSystem.Instance.tree;
        if (_tree == null)
            throw new Exception();

        _camera = Camera.main.GetComponent<CameraController>();
        _mouse = gameController.GetComponent<MouseManager>();
        Focus(_tree.root);
    }

    void OnGUI() {
        GUI.Box(_panelRect, "Solar System");

        var x = _panelRect.x + 10;
        var y = _panelRect.y + 30;

        var buttonPosition = new Vector2(x, y);
        y += DrawRecursiveTreeNode((int)buttonPosition.x, (int)buttonPosition.y, _tree.root);

        GUI.Label(new Rect(x, y, 100, 20), _mouse.selectedObject != null ? _mouse.selectedObject.GetComponent<KeplerTreeNode>().designation : "");
    }

    private int DrawRecursiveTreeNode(int x, int y, KeplerTreeNode data, int recursionLevel = 0) {
        var buttonWidth = 100;
        var buttonHeight = 20;
        var buttonMargin = 5;
        var indentMargin = 10;

        if (GUI.Button(new Rect(x + recursionLevel * indentMargin, y, buttonWidth, buttonHeight), data.designation)) {
            Focus(data);
        }

        y += buttonHeight + buttonMargin;

        foreach (var datum in _tree.GetChildren(data).OrderBy(s => s.distanceFromParent)) {
            y = DrawRecursiveTreeNode(x, y, datum, recursionLevel + 1);
        }

        return y;
    }

    private void Focus(KeplerTreeNode node) {
        var focusDistance = 2;

        _camera.Track(node.transform, focusDistance * node.radius * SolarSystem.ScaleRadius);
    }
}
