using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedCelestialInfo : MonoBehaviour {

    public GameObject gameController;

    private MouseManager _mouse;
    private Rect _panelRect;
    private KeplerTreeNode _focusedBody;

    void Start() {
        _mouse = gameController.GetComponent<MouseManager>();
        if (_mouse == null)
            throw new Exception();

        _mouse.onSelect.AddListener(() => _focusedBody = _mouse.selectedObject.GetComponent<KeplerTreeNode>());
        _mouse.onDeselect.AddListener(() => _focusedBody = null);

        _panelRect = new Rect(Screen.width - 200, Screen.height - 200, 190, 190);
    }

    void OnGUI() {
        if (_focusedBody == null)
            return;

        Display();
    }

    private void Display() {
        GUI.Box(_panelRect, "Body Info");

        var x = _panelRect.x + 10;
        var y = _panelRect.y + 30;

        var labelHeight = 20;
        var labels = GetNodeData();

        foreach (var labelText in labels) {
            GUI.Label(new Rect(x, y, 170, labelHeight), labelText);
            y += labelHeight;
        }
    }

    private List<string> GetNodeData() {
        return new List<string> {
            "DESIGNATION",
            _focusedBody.designation,
            "RADIUS",
            _focusedBody.radius.ToString(),
            "MASS",
            _focusedBody.mass.ToString(),
            "ORBIT DISTANCE",
            _focusedBody.distanceFromParent.ToString()
        };
    }
}
