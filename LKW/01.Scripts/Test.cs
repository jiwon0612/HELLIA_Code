using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public UnityEvent TestEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TestEvent?.Invoke();
        }
    }
}
