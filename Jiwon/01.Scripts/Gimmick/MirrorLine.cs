using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MirrorLine : MonoBehaviour
{
    private List<LineRenderer> _lines;

    private void Awake()
    {
        _lines = GetComponentsInChildren<LineRenderer>(true).ToList();
        SetLine(false);
    }

    public void SetLine(bool isActive)
    {
        for (int i = 0; i < _lines.Count; i++)
        {
            _lines[i].gameObject.SetActive(isActive);
        }
    }
}
