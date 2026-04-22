using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    protected Entity _entity;
    protected AnimationParameterSO _animationParameter;
    protected EntityRenderer _renderer;

    protected bool _isTriggerCall;

    public EntityState(Entity entity, AnimationParameterSO animationParameter)
    {
        _entity = entity;
        _animationParameter = animationParameter;
        _renderer = _entity.GetCompo<EntityRenderer>();
    }
    
    public virtual void Enter()
    {
        _renderer.SetParameter(_animationParameter, true);
        _isTriggerCall = false;
    }
    
    public virtual void Update()
    {
    }
    
    public virtual void Exit()
    {
        _renderer.SetParameter(_animationParameter, false);
    }
    
    public virtual void AnimationEndTrigger()
    {
        _isTriggerCall = true;
    }
}
