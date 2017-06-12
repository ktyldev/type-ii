using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Orbit {
    private float _angle;
    private float _speed;
    private Transform _centre;
    private float _scaledDistance;

    public Orbit(KeplerTreeNode parent, KeplerTreeNode child) {
        var distance = child.distanceFromParent / SolarSystem.ScaleDistance;

        _centre = parent.transform;
        _speed = Mathf.Sqrt(parent.mass / distance);

        var depth = 0;
        var current = _centre;
        while (current != null) {
            depth++;
            current = current.parent;
        }

        _scaledDistance = distance * Mathf.Pow(depth, SolarSystem.ScaleOrbits);
    }

    public Vector3 Increment(float deltaTime) {
        _angle += _speed * deltaTime * -1; // orbit anti-clockwise :)
        return _centre.position + new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle)) * _scaledDistance;
    }
}
