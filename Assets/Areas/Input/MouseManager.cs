using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    private SelectionManager _selection;
    
    void Start()
    {
        _selection = GetComponent<SelectionManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                _selection.Deselect();
                return;
            }

            var hitObject = hit.transform.gameObject;
            var root = hit.transform.root.gameObject;

            while (hitObject.GetComponent<OrbitalBody>() == null)
            {
                if (hitObject == root)
                    return;

                hitObject = hitObject.transform.parent.gameObject;
            }

            _selection.Select(hitObject);
        }
    }
}
