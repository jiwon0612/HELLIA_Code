using UnityEngine;

public class PlayerWallState : EntityState
{
    protected Player _player;
    protected EntityMovement _movement;

    public PlayerWallState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
        _player = entity as Player;
        _movement = entity.GetCompo<EntityMovement>();
    }

    public override void Enter()
    {
        base.Enter();

        _movement.SetGravityScale(0);
        _movement.StopMovement(true);
        _movement.SpeedMultiplier = 0.75f;
    }

    public override void Exit()
    {
        _movement.SetGravityScale(1f);
        _movement.SpeedMultiplier = 1f;

        base.Exit();
    }
}