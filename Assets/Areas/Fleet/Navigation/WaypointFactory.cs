using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFactory : MonoBehaviour {

    public GameObject waypoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Waypoint BuildWaypoint(Fleet fleet, OrbitalBody target) {
        var newWaypoint = Instantiate(waypoint, target.transform).GetComponent<Waypoint>();
        newWaypoint.fleet = fleet;

        var orbitDistance = SolarSystem.Instance.GetFleetOrbitDistance(target);
        newWaypoint.transform.Translate(Vector3.up * orbitDistance);
        return newWaypoint;
    }
}
