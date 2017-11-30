using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform panelRoot;

    private Vector3 _dragOffset;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragOffset = Input.mousePosition - panelRoot.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        panelRoot.position = Input.mousePosition - _dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragOffset = Vector3.zero;
    }
}
