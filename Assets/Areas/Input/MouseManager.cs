using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour {
    public GameObject selectedObject;

    public UnityEvent onSelect;
    public UnityEvent onDeselect;

    void Awake() {
        onSelect = new UnityEvent();
        onDeselect = new UnityEvent();
    }

    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                var hitObject = hit.transform.gameObject;
                var root = hit.transform.root.gameObject;

                while (hitObject.GetComponent<OrbitalBody>() == null) {
                    if (hitObject == root)
                        return;

                    hitObject = hitObject.transform.parent.gameObject;
                }

                SelectObject(hitObject);
            } else {
                ClearSelection();
            }
        }
    }

    private void SelectObject(GameObject go) {
        ClearSelection();
        selectedObject = go;
        onSelect.Invoke();
    }

    private void ClearSelection() {
        onDeselect.Invoke();
        selectedObject = null;
    }
}
