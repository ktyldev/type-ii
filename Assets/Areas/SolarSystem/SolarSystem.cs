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
    public float scaleOrbits = 2;
    public static float ScaleRadius { get { return _instance.scaleRadius; } }
    public static float ScaleDistance { get { return _instance.scaleDistance; } }
    public static float ScaleOrbits { get { return _instance.scaleOrbits; } }

    public float timeWarp;
    public static float TimeWarp { get { return _instance.timeWarp; } }
    public GameObject root;

    public KeplerTreeNode tree { get; private set; }
    
    void Awake() {
        if (_instance != null)
            throw new Exception();

        _instance = this;
        BuildTree();
    }

    private void BuildTree() {
        tree = Instantiate(root, transform).GetComponent<KeplerTreeNode>();
        tree.isRoot = true;
        tree.BuildSatellites();
    }
}
