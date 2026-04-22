using UnityEngine;

public class PlayerParkourState : PlayerAirState
{
    public PlayerParkourState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _movement.StopMovement(true);
        _movement.CanManualMove = false;
        _movement.SetGravityScale(0f);

        _movement.AddForce(new Vector2(-_renderer.FacingDirection * 35, _player.jumpPower * 1.25f));
    }

    public override void Update()
    {
        if (_movement.Velocity.y < 0)
            _player.ChangeState("Fall");
    }

    public override void Exit()
    {
        _movement.CanManualMove = true;
        _renderer.FlipController(-_renderer.FacingDirection);
        _movement.SetGravityScale(1);

        base.Exit();
    }
}