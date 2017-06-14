using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour {

    private KeplerTreeNode _target;
    
    private bool _inOrbit;
    private float _orbitDistance = 1.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_inOrbit) {
            transform.position = SolarSystem.Instance.GetFleetOrbit(_target);
        }
	}

    public void SetTarget(KeplerTreeNode target) {
        _inOrbit = true;
        _target = target;
    }
}
