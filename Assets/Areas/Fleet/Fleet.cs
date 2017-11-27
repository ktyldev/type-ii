using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fleet : MonoBehaviour {

    public float speed;

    private OrbitalBody _target;
    private WaypointFactory _waypoints;
    private Waypoint _waypoint;
    private SolarSystem _solarSystem;

    // Use this for initialization
    void Start () {
        _waypoints = GameObject.FindGameObjectWithTag(GameTags.GameController).GetComponentInChildren<WaypointFactory>();
        _solarSystem = GameObject.FindGameObjectWithTag(GameTags.SolarSystem).GetComponent<SolarSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_waypoint == null) {
            var distance = _solarSystem.GetFleetOrbitDistance(_target);
            var translation = new Vector3(0, distance, 0);
            transform.position = _target.transform.position + translation;

            if (Input.GetKeyDown(KeyCode.Space)) {
                var tree = _solarSystem.tree;

                var currentDepth = tree.GetDepth(_target);
                _target = tree.GetAll()
                    .Where(p => p != _target)
                    .Where(p => tree.GetDepth(p) == currentDepth)
                    .OrderByDescending(p => p.distanceFromParent)
                    .First();
                
                _waypoint = _waypoints.BuildWaypoint(this, _target);
            }
        } else {
            transform.LookAt(_waypoint.transform);
            transform.Translate(Vector3.forward * speed * _solarSystem.DeltaTime);
        }
	}

    public void Spawn(OrbitalBody target) {
        _target = target;
    }
}
