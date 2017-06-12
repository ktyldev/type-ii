using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeplerTreeNode : MonoBehaviour {
    public string designation;
    public GameObject geometry;
    public GameObject[] satelliteTemplates;
    public float radius;
    public float mass;
    public float distanceFromParent;

    private GameObject _geometry;
    private List<KeplerTreeNode> _satellites;

    public KeplerTreeNode parent { get { return GetComponentInParent<KeplerTreeNode>(); } }

    void Awake() {
        _satellites = new List<KeplerTreeNode>();

        _geometry = Instantiate(geometry, transform);
        _geometry.transform.localScale *= SolarSystem.ScaleRadius * radius;
    }

    public List<KeplerTreeNode> Satellites() {
        return _satellites;
    }

    public void AddSatellite(KeplerTreeNode satellite) {
        _satellites.Add(satellite);
    }
}
