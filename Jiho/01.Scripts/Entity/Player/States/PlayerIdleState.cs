using UnityEngine;

public class PlayerIdleState : EntityState
{
    private Player _player;
    private EntityMovement _movement;
    
    public PlayerIdleState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
        _player = entity as Player;
        _movement = entity.GetCompo<EntityMovement>();
    }

    public override void Enter()
    {
        base.Enter();

        _movement.StopMovement();
        _movement.SetGravityScale(1);
    }

    public override void Update()
    {
        base.Update();
        
        var xMove = _player.PlayerInput.InputMovement.x;
        
        if (Mathf.Abs(xMove) > 0)
            _player.ChangeState("Move");
    }
}
