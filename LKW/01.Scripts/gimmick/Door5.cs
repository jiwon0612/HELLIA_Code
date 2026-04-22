using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door5 : MonoBehaviour,IInitializable
{
    [SerializeField] private int _openCount;
    [field:SerializeField]public int CurrentCount { get; set; } = 4;
    [SerializeField] private int moveDistance = 3;
    public bool isOpen;
    private Vector3 _originPos;
    
    public bool isHorizon = false;


    private void Start()
    {
        _originPos = transform.position;
    }

    private void Update()
    {
        MoveDoor();
    }

    private void MoveDoor()
    {
        if (CurrentCount <= 0 && !isOpen && !isHorizon)
        {
            transform.DOMoveY(transform.position.y + moveDistance, 0.6f)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            isOpen = true;
        }
        else if (CurrentCount <= 0 && !isOpen && isHorizon)
        {
            transform.DOMoveX(transform.position.x + moveDistance, 0.6f)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            isOpen = true;
        }
    }


    public void Initialize()
    {
        CurrentCount = _openCount;
        gameObject.SetActive(true);
        transform.position = _originPos;
        isOpen = false;
    }
}
