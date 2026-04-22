using UnityEngine;

public class PlayerDashState : EntityState
{
    private Player _player;
    private EntityMovement _movement;

    private float _dashStartTime;

    public PlayerDashState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
        _player = entity as Player;
        _movement = entity.GetCompo<EntityMovement>();
    }

    public override void Enter()
    {
        base.Enter();

        _movement.SetGravityScale(0);
        _movement.StopMovement(true);
        _movement.CanManualMove = false;

        PlayerDashEffect.isDash = true;
        
        var dashSpeed = GetInputDirection() * _player.dashSpeed;

        _movement.AddForce(dashSpeed);
        _dashStartTime = Time.time;
    }

    private Vector2 GetInputDirection()
    {
        var inputDir = _player.PlayerInput.InputMovement.normalized;

        if (inputDir == Vector2.zero)
            inputDir = new Vector2(_renderer.FacingDirection, 0);

        return inputDir;
    }

    public override void Update()
    {
        base.Update();

        if (_dashStartTime + _player.dashDuration < Time.time)
            _player.ChangeState("Fall");
    }

    public override void Exit()
    {
        _movement.CanManualMove = true;
        _movement.SetGravityScale(1f);
        _movement.StopMovement(true);

        PlayerDashEffect.isDash = false;

        base.Exit();
    }
}