using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyInfoPanel : MonoBehaviour
{
    public string noValue;
    public Text designation;
    public Text radius;
    public Text mass;
    public Text orbitDistance;
    
    void Start()
    {
        var selection = GameObject.FindGameObjectWithTag(GameTags.Input).GetComponent<SelectionManager>();

        selection.OnSelect.AddListener(go =>
        {
            var body = go.GetComponent<OrbitalBody>();
            if (body == null)
            {
                print("body null");
                return;
            }

            designation.text = body.designation;
            radius.text = body.radius.ToString();
            mass.text = body.mass.ToString();
            orbitDistance.text = body.distanceFromParent.ToString();
        });

        designation.text = noValue;
        radius.text = noValue;
        mass.text = noValue;
        orbitDistance.text = noValue;
    }
}
