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
        _rows = OrderRows(_rows.ToArray()).Reverse().ToList();

        for (int i = 0; i < _rows.Count; i++)
        {
            var y = (1 + i) * rowHeight;
            var oldPos = _rows[i].transform.position;
            _rows[i].transform.position = new Vector3
            {
                x = oldPos.x,
                y = transform.position.y + y,
                z = 0
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
