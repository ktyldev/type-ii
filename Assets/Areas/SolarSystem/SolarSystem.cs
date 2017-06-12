using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// READ: the universe. This is the root object for orbital movement.
/// </summary>
public class SolarSystem : MonoBehaviour {
    private static SolarSystem _instance;

    // Number of real units are represented by a game unit
    public float scaleRadius = 0.5f;
    public float scaleDistance = 1000000;
    public static float ScaleRadius { get { return _instance.scaleRadius; } }
    public static float ScaleDistance { get { return _instance.scaleDistance; } }

    public float timeWarp;
    public GameObject root;

    public KeplerTreeNode tree { get; private set; }

    private List<KeyValuePair<KeplerTreeNode, Orbit>> _orbits;

    void Awake() {
        if (_instance != null)
            throw new Exception();

        _instance = this;

        _orbits = new List<KeyValuePair<KeplerTreeNode, Orbit>>();
        tree = InstantiateKeplerBody(root);
    }

    void Update() {
        IncrementOrbits();
    }

    private void IncrementOrbits() {
        foreach (var kvp in _orbits) {
            kvp.Key.transform.position = kvp.Value.Increment(Time.deltaTime * timeWarp);
        }
    }

    private KeplerTreeNode InstantiateKeplerBody(GameObject template, KeplerTreeNode parent = null) {
        var body = Instantiate(template, parent == null ? transform : parent.transform)
            .GetComponent<KeplerTreeNode>();

        if (body == null)
            throw new Exception();

        if (parent != null) {
            parent.AddSatellite(body);
            var orbit = new Orbit(parent, body);
            _orbits.Add(new KeyValuePair<KeplerTreeNode, Orbit>(body, orbit));
        }

        foreach (var satelliteTemplate in body.satelliteTemplates) {
            InstantiateKeplerBody(satelliteTemplate, body);
        }

        return body;
    }
}
