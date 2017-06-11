using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// READ: the universe. This is the root object for orbital physics.
/// </summary>
public class SolarSystem : MonoBehaviour {
    public float timeWarp;
    public GameObject root;

    private KeplerBody _root;

    private List<KeyValuePair<KeplerBody, Orbit>> _orbits;

    void Awake() {
        _orbits = new List<KeyValuePair<KeplerBody, Orbit>>();
    }

    void Start() {
        _root = InstantiateKeplerBody(root);
    }
    
    void Update() {
        IncrementOrbits();
    }

    private void IncrementOrbits() {
        foreach (var kvp in _orbits) {
            kvp.Key.transform.position = kvp.Value.Increment(Time.deltaTime * timeWarp);
        }
    }

    private KeplerBody InstantiateKeplerBody(GameObject template, KeplerBody parent = null) {
        var body = Instantiate(template, parent == null ? transform : parent.transform)
            .GetComponent<KeplerBody>();

        if (body == null)
            throw new Exception();
        
        if (parent != null) {
            var orbit = new Orbit(parent, body.distanceFromParent);
            _orbits.Add(new KeyValuePair<KeplerBody, Orbit>(body, orbit));
        }

        foreach (var satelliteTemplate in body.satelliteTemplates) {
            InstantiateKeplerBody(satelliteTemplate, body);
        }

        return body;
    }
}
