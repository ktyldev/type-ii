using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour {

    public Fleet fleet { get; set; }

    public UnityEvent onArrive { get; private set; }
    
    void Awake() {
        onArrive = new UnityEvent();
    }

    void Start() {

    }

    void OnTriggerEnter(Collider other) {
        if (fleet == null)
            throw new Exception();

        var otherFleet = other.GetComponent<Fleet>();
        if (otherFleet == null)
            return;

        else if (otherFleet == fleet){
            onArrive.Invoke();
            Destroy(gameObject);
        }
    }
}
