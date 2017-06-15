using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetFactory : MonoBehaviour {

    public GameObject fleet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Fleet BuildFleet(OrbitalBody buildLocation) {
        var newFleet = Instantiate(fleet, transform).GetComponent<Fleet>();
        newFleet.SetTarget(buildLocation);
        return newFleet;
    }
}
