using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour {

    public SelectionEvent OnSelect { get; private set; }
    public DeselectionEvent OnDeselect { get; private set; }
    
    private GameObject _selectedObject;

    void Awake()
    {
        OnSelect = new SelectionEvent();
        OnDeselect = new DeselectionEvent();
    }
    
    public void Select(GameObject obj)
    {
        if (_selectedObject != null)
        {
            // Deselect();
        }

        _selectedObject = obj;
        OnSelect.Invoke(obj);
    }
    
    public void Deselect()
    {
        if (_selectedObject == null)
            return;

        OnDeselect.Invoke(_selectedObject);
        _selectedObject = null;
    }

    public class SelectionEvent : UnityEvent<GameObject>
    { }

    public class DeselectionEvent : UnityEvent<GameObject>
    { }
}
