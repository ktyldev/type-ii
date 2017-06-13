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

    private bool _isRoot;
    private GameObject _geometry;
    private Orbit _orbit;
    private LineRenderer _orbitRenderer;

    void Awake() {
        _geometry = Instantiate(geometry, transform);
        _geometry.transform.localScale *= SolarSystem.ScaleRadius * radius;

        _orbitRenderer = GetComponent<LineRenderer>();
    }

    void Start() {
        _isRoot = SolarSystem.Instance.tree.GetDepth(this) == 0;
        if (!_isRoot) {
            _orbit = new Orbit(this);
        }
    }

    void Update() {
        if (_isRoot)
            return;

        _orbit.Draw(_orbitRenderer);
        transform.position = _orbit.Increment(Time.deltaTime * SolarSystem.TimeWarp);
    }
}
