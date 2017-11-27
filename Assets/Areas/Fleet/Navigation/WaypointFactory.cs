using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFactory : MonoBehaviour
{
    public GameObject waypoint;

    private SolarSystem _solarSystem;

    void Start()
    {
        _solarSystem = GameObject.FindGameObjectWithTag(GameTags.SolarSystem).GetComponent<SolarSystem>();
    }
    
    public Waypoint BuildWaypoint(Fleet fleet, OrbitalBody target)
    {
        var newWaypoint = Instantiate(waypoint, target.transform).GetComponent<Waypoint>();
        newWaypoint.fleet = fleet;

        var orbitDistance = _solarSystem.GetFleetOrbitDistance(target);
        newWaypoint.transform.Translate(Vector3.up * orbitDistance);
        return newWaypoint;
    }
}
