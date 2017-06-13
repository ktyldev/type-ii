using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MouseManager : MonoBehaviour {
    public GameObject selectedObject;

    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                var hitObject = hit.transform.gameObject;
                var root = hit.transform.root.gameObject;

                while (hitObject.GetComponent<KeplerTreeNode>() == null) {
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
        selectedObject = go;
    }

    private void ClearSelection() {
        selectedObject = null;
    }
}
