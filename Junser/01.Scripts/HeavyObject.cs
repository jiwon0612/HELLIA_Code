using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyObject : MonoBehaviour, IHasWeight, IInitializable
{
    [SerializeField]
    private int weight;
    [SerializeField]
    private float checkDistance;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField] 
    LayerMask anotherHeavyObject;
    [SerializeField]
    private GimmickSoundDataSO so;

    private Vector2 originPos;
    private Quaternion originRotation;

    private Collider2D detectedCollider = null;
    private bool exitFlag;
    private void Start()
    {
        originPos = transform.position;
        originRotation = transform.rotation;
        detectedCollider = null;
    }
    private void Update()
    {
        CheckFragileFlatForm(Vector2.down, checkDistance, layerMask);
        DetectionLength(Vector2.down, checkDistance, anotherHeavyObject);
    }

    private void DetectionLength(Vector2 rayDir, float checkLength, LayerMask checkingLayer)
    {
        RaycastHit2D[] heabyOjects = Physics2D.RaycastAll (transform.position, rayDir, checkLength, checkingLayer);
        checkDistance = heabyOjects.Length -0.45f;
    }

    public void CheckFragileFlatForm(Vector2 rayDir, float checkLength, LayerMask checkingLayer)
    {
        RaycastHit2D detected = Physics2D.Raycast(transform.position, rayDir, checkLength, checkingLayer);
        if(detectedCollider == detected.collider) return;
        bool enter = detectedCollider == null;
        detectedCollider =enter ? detected.collider: detectedCollider;

        detectedCollider.TryGetComponent<IFragile>(out IFragile fragile);

        Action behave = null;
        if(enter){ behave = () => fragile.AddWeight(weight); exitFlag = false;}
        else if (exitFlag == false) { behave = () => fragile.ReduceWeight(weight); exitFlag = true; detectedCollider = null;}
        FragileBehave(behave);
    }

    public void FragileBehave(Action behave)
    {
        if(behave == null) return;
        behave();
    }

    public void Initialize()
    {
        detectedCollider = null ;
        transform.position = originPos;
        transform.rotation = originRotation;
    }
}
