using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovePlatformX : FloorTrap
{
    [SerializeField] private float _moveXDistance = 2f;
    [SerializeField] private float _moveDuration = 1.5f;
    [SerializeField] private int directionX = 1;

    private Sequence _moveSequence;

    private void OnEnable()
    {
        _moveSequence = DOTween.Sequence();
        _moveSequence.Append(transform.DOMoveX(transform.position.x + _moveXDistance * directionX
                , _moveDuration, false)
            .SetEase(Ease.Linear));

        _moveSequence.Append(transform.DOMoveX(transform.position.x * directionX
                , _moveDuration,false)
            .SetEase(Ease.Linear));
        
        _moveSequence.SetLoops(-1);
    }

    private void OnDisable()
    {
        _moveSequence?.Kill();
    }

    protected override void FloorExit(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & whatIsTarget) != 0)
        {
            collision.gameObject.transform.SetParent(null);
        }
        base.FloorExit(collision);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        FloorEnter(collision);
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            FloorExit(collision);
        }
    }

    protected override void FloorEnter(Collision2D collision)
    {
        collision.transform.SetParent(transform);

    }
}
