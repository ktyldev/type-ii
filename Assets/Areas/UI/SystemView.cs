using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TypeII;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class SystemView : MonoBehaviour {

    public GameObject gameController;
    public float panelHeight;
    public float panelWidth;
    public float margin;

    public float nodeWidth;
    public float nodeIndent;

    private RecursiveTree<OrbitalBody> _tree;
    private Rect _panelRect;
    private CameraController _camera;

    void Awake() {
        _panelRect = new Rect(margin, margin, panelWidth, panelHeight);
    }

    // Use this for initialization
    void Start() {
        _tree = SolarSystem.Instance.tree;
        if (_tree == null)
            throw new Exception();

        _camera = Camera.main.GetComponent<CameraController>();
        Focus(_tree.root);
    }

    void OnGUI() {
        GUI.Box(_panelRect, "Solar System");

        var x = _panelRect.x + 10;
        var y = _panelRect.y + 30;

        var buttonPosition = new Vector2(x, y);
        y += DrawRecursiveTreeNode((int)buttonPosition.x, (int)buttonPosition.y, _tree.root);
    }

    private int DrawRecursiveTreeNode(int x, int y, OrbitalBody data, int recursionLevel = 0) {
        var buttonHeight = 20;
        var buttonMargin = 5;

        if (GUI.Button(new Rect(x + recursionLevel * nodeIndent, y, nodeWidth, buttonHeight), data.designation)) {
            Focus(data);
        }

        y += buttonHeight + buttonMargin;

        foreach (var datum in _tree.GetChildren(data).OrderBy(s => s.distanceFromParent)) {
            y = DrawRecursiveTreeNode(x, y, datum, recursionLevel + 1);
        }

        return y;
    }

    private void Focus(OrbitalBody node) {
        var focusDistance = 2;
        _camera.Track(node.transform, focusDistance * SolarSystem.Instance.GetScaledRadius(node.radius));
    }
}
