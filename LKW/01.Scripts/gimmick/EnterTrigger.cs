using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnterTrigger : MonoBehaviour,IInitializable
{
    public UnityEvent EnterEvent;
    [SerializeField] private LayerMask _whatIsTarget;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnterEvent?.Invoke();
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
    }
}
