using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRenderer : MonoBehaviour, IEntityComponent
{
    public float FacingDirection { get; private set; } = 1; // 오른쪽 1, 왼쪽 -1
    public SpriteRenderer EntitySprite {get; private set;}

    private Entity _entity;
    private Animator _animator;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;

        _animator = GetComponent<Animator>();
        EntitySprite = GetComponent<SpriteRenderer>();
    }
    
    public void SetParameter(AnimationParameterSO param, bool value) => _animator.SetBool(param.hashValue, value);
    public void SetParameter(AnimationParameterSO param, float value) => _animator.SetFloat(param.hashValue, value);
    public void SetParameter(AnimationParameterSO param, int value) => _animator.SetInteger(param.hashValue, value);
    public void SetParameter(AnimationParameterSO param) => _animator.SetTrigger(param.hashValue);

    private void Flip()
    {
        FacingDirection *= -1;
        _entity.transform.Rotate(0, 180f, 0);
    }

    public void FlipController(float xMove)
    {
        if (Mathf.Abs(FacingDirection + xMove) < 0.5f)
            Flip();
    }
}
