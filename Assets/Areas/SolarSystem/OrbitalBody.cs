using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrbitalBody : MonoBehaviour
{
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
    private SolarSystem _solarSystem;

    void Start()
    {
        _solarSystem = GameObject.FindGameObjectWithTag(GameTags.SolarSystem).GetComponent<SolarSystem>();

        _orbitRenderer = GetComponent<LineRenderer>();

        _geometry = Instantiate(geometry, transform);
        _geometry.transform.localScale *= _solarSystem.GetScaledRadius(radius) * 2;

        _isRoot = _solarSystem.tree.GetDepth(this) == 0;
        if (!_isRoot)
        {
            _orbit = _solarSystem.MakeOrbit(this);
        }
    }

    void Update()
    {
        if (_isRoot)
            return;

        _orbit.Draw(_orbitRenderer);
        transform.position = _orbit.Increment(Time.deltaTime * _solarSystem.TimeWarp);
    }
}
