using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TypeII;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// READ: the universe. This is the root object for orbital movement.
/// </summary>
public class SolarSystem : MonoBehaviour {
    
    public float fleetOrbitDistance;

    // Number of real units are represented by a game unit
    public float scaleRadius;
    public float scaleOrbits;
    public float timeWarp;
    
    public GameObject root;
    public float ScaleOrbits { get { return scaleOrbits; } }
    public float TimeWarp { get { return timeWarp; } }
    public float DeltaTime { get { return Time.deltaTime * timeWarp; } }

    public RecursiveTree<OrbitalBody> tree { get; private set; }
    
    void Awake() {
        tree = new RecursiveTree<OrbitalBody>(root, transform, n => n.satellites);
    }

    public float GetOrbitDistance(OrbitalBody node) {
        var depth = tree.GetDepth(node);
        return node.distanceFromParent * Mathf.Pow(depth + 1, scaleOrbits);
    }

    public float GetScaledRadius(float radius) {
        return radius * scaleRadius;
    }

    public Orbit MakeOrbit(OrbitalBody node) {
        return new Orbit(tree.GetParent(node), node.transform, GetOrbitDistance(node));
    }

    public float GetFleetOrbitDistance(OrbitalBody body) {
        return scaleRadius * fleetOrbitDistance * body.radius;
    }
}
