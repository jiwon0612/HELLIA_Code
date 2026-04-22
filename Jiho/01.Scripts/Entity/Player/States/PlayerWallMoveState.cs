using UnityEngine;

public class PlayerWallMoveState : PlayerWallState
{
    public PlayerWallMoveState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
    }

    public override void Update()
    {
        base.Update();

        var yMove = _player.PlayerInput.InputMovement.y;

        _movement.SetYMovement(yMove);

        if (!_movement.IsWall)
            _player.ChangeState("Idle");
        
        if (Mathf.Approximately(yMove, 0))
            _player.ChangeState("WallIdle");
    }

    public override void Exit()
    {
        _movement.AddForce(new Vector2(0, 1) * _player.jumpPower);

        base.Exit();
    }
}