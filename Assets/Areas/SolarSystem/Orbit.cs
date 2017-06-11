using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Orbit {
    private float _angle;
    private float _speed;
    private float _distance;
    private Transform _centre;

    public Orbit(KeplerBody parent, float distance) {
        _centre = parent.transform;
        _distance = distance;
        _speed = Mathf.Sqrt(parent.mass / distance);
    }

    public Vector3 Increment(float deltaTime) {
        _angle += _speed * deltaTime * -1; // orbit anti-clockwise :)
        return _centre.position + new Vector3(Mathf.Sin(_angle), 0, Mathf.Cos(_angle)) * _distance;
    }
}
