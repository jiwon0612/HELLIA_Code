using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerWallState
{
    public PlayerWallSlideState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        _movement.SetGravityScale(2f);
        _player.PlayerInput.JumpEvent += OnParkour;
    }

    public override void Update()
    {
        base.Update();
        
        var xInput = _player.PlayerInput.InputMovement.x;
        var yInput = _player.PlayerInput.InputMovement.y;
        
        _movement.SetXMovement(xInput);

        _movement.SetGravityScale(yInput < 0 ? 5f : 2f);

        if (_movement.IsGround)
            _player.ChangeState("Idle");
    }

    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= OnParkour;
        
        base.Exit();
    }

    private void OnParkour()
    {
        _player.ChangeState("Parkour");
    }
}
