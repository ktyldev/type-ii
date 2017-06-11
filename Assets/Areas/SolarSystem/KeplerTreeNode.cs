using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeplerTreeNode : MonoBehaviour {
    public string designation;
    public float mass;
    public float distanceFromParent;
    public GameObject[] satelliteTemplates;
    public List<KeplerTreeNode> satellites;

    void Awake() {
        satellites = new List<KeplerTreeNode>();
    }
}
