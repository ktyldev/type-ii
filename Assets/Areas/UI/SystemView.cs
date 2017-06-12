using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class SystemView : MonoBehaviour {
    
    private KeplerTreeNode _keplerTree;
    private TreeViewItem _viewRoot;

    private Rect _panelRect;

    void Awake() {
        _panelRect = new Rect(10, 10, 200, Screen.height - 10);
    }

    // Use this for initialization
    void Start() {
        _keplerTree = GameObject.FindGameObjectWithTag("SystemRoot").GetComponent<SolarSystem>().tree;
        if (_keplerTree == null)
            throw new Exception();
    }

    void OnGUI() {
        GUI.Box(_panelRect, "Solar System");

        var buttonPosition = new Vector2(_panelRect.x + 10, _panelRect.y + 30);
        Node((int)buttonPosition.x, (int)buttonPosition.y, _keplerTree);
    }

    private int Node(int x, int y, KeplerTreeNode data, int recursionLevel = 0) {
        var buttonWidth = 100;
        var buttonHeight = 20;
        var buttonMargin = 5;
        var indentMargin = 10;

        if (GUI.Button(new Rect(x + recursionLevel * indentMargin, y, buttonWidth, buttonHeight), data.designation)) {
            Camera.main.GetComponent<CameraController>().Track(data.transform, data.radius * SolarSystem.ScaleRadius);
        }
        y += buttonHeight + buttonMargin;

        if (data.satellites.Any()) {

            foreach (var datum in data.satellites) {
                y = Node(x, y, datum, recursionLevel + 1);
            }
        }

        return y;
    }
}
