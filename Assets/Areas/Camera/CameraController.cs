using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 20;
    public float panBorderThickness = 10;
    public Vector2 panLimit;
    public float zoomSpeed;

    // Update is called once per frame
    void Update () {
        var pos = transform.position;
        
        pos += DirectionInput() * panSpeed * Time.deltaTime;
        pos.y += Scroll();
        
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.y);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
	}

    private Vector3 DirectionInput() {
        var direction = new Vector3();
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness) {
            direction.z = 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness) {
            direction.z = -1;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness) {
            direction.x = 1;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness) {
            direction.x = -1;
        }

        return direction;
    }

    private float Scroll() {
        return Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
    }
}
