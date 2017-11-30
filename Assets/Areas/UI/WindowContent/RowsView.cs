using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RowsView : MonoBehaviour
{
    public int rowHeight;

    private List<GameObject> _rows;

    void Awake()
    {
        _rows = new List<GameObject>();
    }
    
    public void Invalidate()
    {
        _rows = OrderRows(_rows.ToArray()).ToList();

        var topY = ((RectTransform)transform).offsetMax.y;

        for (int i = 0; i < _rows.Count; i++)
        {
            var y = i * rowHeight;
            _rows[i].GetComponent<RectTransform>().localPosition = new Vector3
            {
                x = _rows[i].transform.localPosition.x,
                y = -((RectTransform)transform).rect.y - y
            };
        }
    }
    
    protected void AddRow(GameObject row)
    {
        _rows.Add(row);
    }

    protected void Remove(GameObject row)
    {
        if (!_rows.Contains(row))
            throw new System.Exception();

        _rows.Remove(row);
    }

    protected virtual GameObject[] OrderRows(GameObject[] rows)
    {
        return rows;
    }
}
