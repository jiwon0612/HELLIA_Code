using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeSand : MonoBehaviour
{
    [SerializeField] LayerMask _playerLayer;
    private Collider2D _collider2D;

    public UnityEvent OnSandTaken;
    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OnSandTaken?.Invoke();
    }
}
