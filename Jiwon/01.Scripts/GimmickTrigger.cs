using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class GimmickTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private bool isHorizon;

    public UnityEvent onGimmickStartSetting;
    public UnityEvent onGimmickEndSetting;

    private Vector2 inColVec;

    private void Awake()
    {
        var collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & whatIsTarget) != 0)
        {
            inColVec = (other.bounds.center - transform.position).normalized;
            onGimmickStartSetting?.Invoke();
            onGimmickEndSetting?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & whatIsTarget) != 0)
        {
            Vector2 outColVec = (other.bounds.center - transform.position).normalized;

            if ((Mathf.Approximately(Mathf.Sign(outColVec.x), Mathf.Sign(inColVec.x)) && isHorizon)
                || (Mathf.Approximately(Mathf.Sign(outColVec.y), Mathf.Sign(inColVec.y)) && !isHorizon))
            {
                onGimmickStartSetting?.Invoke();
                onGimmickEndSetting?.Invoke();
            }
        }
    }
}
