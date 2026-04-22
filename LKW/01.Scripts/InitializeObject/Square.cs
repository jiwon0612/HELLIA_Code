using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour,IInitializable
{
    private Vector3 originPos;
    
    private void Start()
    {
        originPos = transform.position;
    }

    public void Initialize()
    {
        transform.position = originPos;
    }
}
