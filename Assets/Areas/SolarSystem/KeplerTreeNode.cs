using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeplerTreeNode : MonoBehaviour {
    public string designation;
    public GameObject geometry;
    public GameObject[] satellites;
    public float radius;
    public float mass;
    public float distanceFromParent;

    public bool isRoot;
    private GameObject _geometry;
    private List<KeplerTreeNode> _satellites;
    private Orbit _orbit;

    public KeplerTreeNode parent { get { return transform.parent.GetComponent<KeplerTreeNode>(); } }

    void Awake() {
        _satellites = new List<KeplerTreeNode>();

        _geometry = Instantiate(geometry, transform);
        _geometry.transform.localScale *= SolarSystem.ScaleRadius * radius;
    }

    void Start() {
        if (isRoot)
            return;

        _orbit = new Orbit(parent, this);
    }

    void Update() {
        if (isRoot)
            return;

        transform.position = _orbit.Increment(Time.deltaTime * SolarSystem.TimeWarp);
    }

    public List<KeplerTreeNode> Satellites() {
        return _satellites;
    }

    public void AddSatellite(KeplerTreeNode satellite) {
        _satellites.Add(satellite);
    }

    public void BuildSatellites() {
        _satellites = satellites.Select(BuildSatellite).ToList();
    }

    private KeplerTreeNode BuildSatellite(GameObject template) {
        var satellite = Instantiate(template, transform).GetComponent<KeplerTreeNode>();
        if (satellite == null)
            throw new Exception();

        satellite.BuildSatellites();
        return satellite;
    }
}
