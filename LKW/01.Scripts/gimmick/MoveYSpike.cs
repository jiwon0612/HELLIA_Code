using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveYSpike : MonoBehaviour
{
    [SerializeField] private float moveYDistance = 2f;
    [SerializeField] private float moveDuration = 1.5f;
    [SerializeField] private float moveDir = 1;
    
    private Sequence _moveSequence;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOLocalMoveY(transform.localPosition.y + moveYDistance * moveDir
            , moveDuration,false).SetEase(Ease.Linear));
        _moveSequence.Append(transform.DOLocalMoveY(transform.localPosition.y
            , moveDuration, false).SetEase(Ease.Linear));
        _moveSequence.SetLoops(-1);
    }

    private void OnDisable()
    {
        this.transform.DOKill();
    }
}