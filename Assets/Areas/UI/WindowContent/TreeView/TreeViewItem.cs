using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TreeViewItem : MonoBehaviour
{

    public bool IsExpanded { get; private set; }
    
    public UnityEvent OnExpand { get; private set; }
    public UnityEvent OnCollapse { get; private set; }

    private OrbitalBody _body;
    public OrbitalBody Body
    {
        get
        {
            return _body;
        }
        set
        {
            _body = value;

            GetComponent<Text>().text = value.designation;
            name = value.designation;
        }
    }

    void Awake()
    {
        OnExpand = new UnityEvent();
        OnCollapse = new UnityEvent();
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Select);
    }

    private void Select()
    {
        if (_body == null)
            throw new System.Exception();

        GameObject.FindGameObjectWithTag(GameTags.Input)
            .GetComponent<SelectionManager>()
            .Select(_body.gameObject);
    }

    public void Expand()
    {
        OnExpand.Invoke();
        IsExpanded = true;
    }

    public void Collapse()
    {
        OnCollapse.Invoke();
        IsExpanded = false;
    }
}
