using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallIdleState : PlayerWallState
{
    public PlayerWallIdleState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        _player.soundData.PlaySound(SoundType.PlayerClimb);
    }

    public override void Update()
    {
        base.Update();
        
        var yMove = _player.PlayerInput.InputMovement.y;
        
        if (!_player.PlayerInput.isCatchKeyPressed || !_movement.IsWall)
            _player.ChangeState("Idle");
        
        if (Mathf.Abs(yMove) > 0 && _movement.IsWall)
            _player.ChangeState("WallMove");
    }
}
