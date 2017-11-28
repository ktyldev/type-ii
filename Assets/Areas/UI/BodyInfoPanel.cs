using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyInfoPanel : MonoBehaviour
{
    public Text designation;
    public Text radius;
    public Text mass;
    public Text orbitDistance;

    private MouseManager _mouse;
    private OrbitalBody _focusedBody;

    void Start()
    {
        _mouse = GameObject.FindGameObjectWithTag(GameTags.Input).GetComponent<MouseManager>();
        if (_mouse == null)
            throw new Exception();

        _mouse.onSelect.AddListener(go => _focusedBody = go.GetComponent<OrbitalBody>());
        _mouse.onDeselect.AddListener(_ => _focusedBody = null);
    }

    void OnGUI()
    {
        if (_focusedBody == null)
            return;

        designation.text = _focusedBody.designation;
        radius.text = _focusedBody.radius.ToString();
        mass.text = _focusedBody.mass.ToString();
        orbitDistance.text = _focusedBody.distanceFromParent.ToString();
    }
}
