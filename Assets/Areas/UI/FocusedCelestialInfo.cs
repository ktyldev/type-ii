using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusedCelestialInfo : MonoBehaviour {

    public GameObject gameController;
    public float panelWidth;
    public float panelHeight;
    public float margin;

    public float labelLength;
    
    private MouseManager _mouse;
    private Rect _panelRect;
    private KeplerTreeNode _focusedBody;

    void Start() {
        _mouse = gameController.GetComponent<MouseManager>();
        if (_mouse == null)
            throw new Exception();

        _mouse.onSelect.AddListener(() => _focusedBody = _mouse.selectedObject.GetComponent<KeplerTreeNode>());
        _mouse.onDeselect.AddListener(() => _focusedBody = null);

        _panelRect = new Rect(
            Screen.width - (panelWidth + margin), 
            Screen.height - (panelHeight + margin), 
            panelWidth, 
            panelHeight);
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
            GUI.Label(new Rect(x, y, labelLength, labelHeight), labelText);
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
