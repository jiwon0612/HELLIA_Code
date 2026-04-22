using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    private float _jumpTime;
    private readonly float _maxJumpTime = 0.5f;

    public PlayerJumpState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _jumpTime = 0;

        _movement.StopMovement(true);
        _movement.AddForce(new Vector2(0, _player.jumpPower));

        _movement.SetGravityScale(0.5f);
    }

    public override void Update()
    {
        base.Update();

        if (_player.PlayerInput.isJumpKeyPressed)
        {
            _jumpTime += Time.deltaTime;
        
            if (_jumpTime > _maxJumpTime)
                EndJump();
        }
        else
            EndJump();
        
        if (_movement.Velocity.y < 0)
            EndJump();
    }

    public override void Exit()
    {
        _movement.SetGravityScale(1f);

        base.Exit();
    }

    private void EndJump()
    {
        _movement.SetGravityScale(1f);

        _player.ChangeState("Fall");
    }
}