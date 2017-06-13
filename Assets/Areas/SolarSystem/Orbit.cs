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
        var distance = child.distanceFromParent;

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

    public void Draw(LineRenderer renderer) {
        var segments = 100;
        
        float x, z;
        float angle = 0;
        renderer.positionCount = segments + 1;
        for (int i = 0; i < segments + 1; i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _scaledDistance;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * _scaledDistance;

            renderer.SetPosition(i, new Vector3(x, 0, z) + _centre.position);
            angle += (360f / segments);
        }
    }
}
