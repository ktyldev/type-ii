using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TreeViewItem : MonoBehaviour {
    
    public bool IsExpanded { get; private set; }

    public string Text {
        set
        {
            GetComponent<Text>().text = value;
            name = value;
        }
    }

    public UnityEvent OnExpand { get; private set; }
    public UnityEvent OnCollapse { get; private set; }

    public OrbitalBody Body { get; set; }

    void Awake()
    {
        OnExpand = new UnityEvent();
        OnCollapse = new UnityEvent();
    }

    // Use this for initialization
    void Start () {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => Camera.main.GetComponent<CameraController>().Track(Body.transform));
	}
	
	// Update is called once per frame
	void Update () {
		
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
