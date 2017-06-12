using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeplerTreeNode : MonoBehaviour {
    public string designation;
    public float radius;
    public float mass;
    public float distanceFromParent;
    public GameObject geometry;
    private GameObject _geometry;

    public GameObject[] satelliteTemplates;
    private List<KeplerTreeNode> _satellites;
    public List<KeplerTreeNode> satellites { get { return _satellites; } }
    

    void Awake() {
        _satellites = new List<KeplerTreeNode>();

        _geometry = Instantiate(geometry, transform);
        _geometry.transform.localScale *= SolarSystem.ScaleRadius * radius;
    }
}
