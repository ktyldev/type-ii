using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    // public GameObject selectedObject;

    public MouseSelectionEvent onSelect;
    public MouseDeselectionEvent onDeselect;

    private GameObject _selectedObject;

    void Awake()
    {
        onSelect = new MouseSelectionEvent();
        onDeselect = new MouseDeselectionEvent();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hitObject = hit.transform.gameObject;
                var root = hit.transform.root.gameObject;

                while (hitObject.GetComponent<OrbitalBody>() == null)
                {
                    if (hitObject == root)
                        return;

                    hitObject = hitObject.transform.parent.gameObject;
                }

                _selectedObject = hitObject;
                onSelect.Invoke(_selectedObject);
            }
            else
            {
                onDeselect.Invoke(_selectedObject);
                _selectedObject = null;
            }
        }
    }
    
    public class MouseSelectionEvent : UnityEvent<GameObject>
    {
    }

    public class MouseDeselectionEvent : UnityEvent<GameObject>
    {
    }
}
