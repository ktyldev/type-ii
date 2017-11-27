﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusedCelestialInfo : MonoBehaviour
{
    public Text designation;
    public Text radius;
    public Text mass;
    public Text orbitDistance;
    
    private MouseManager _mouse;
    private OrbitalBody _focusedBody;

    void Start()
    {
        _mouse = GameObject.FindGameObjectWithTag(GameTags.GameController).GetComponent<MouseManager>();
        if (_mouse == null)
            throw new Exception();

        _mouse.onSelect.AddListener(() => _focusedBody = _mouse.selectedObject.GetComponent<OrbitalBody>());
        _mouse.onDeselect.AddListener(() => _focusedBody = null);
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
