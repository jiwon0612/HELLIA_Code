using UnityEngine;

public class PlayerMoveState : EntityState
{
    private Player _player;
    private EntityMovement _movement;
    
    public PlayerMoveState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
        _player = entity as Player;
        _movement = _player.GetCompo<EntityMovement>();
    }
    
    public override void Update()
    {
        base.Update();

        var xMove = _player.PlayerInput.InputMovement.x;

        _movement.SetXMovement(xMove);

        if (Mathf.Approximately(xMove, 0))
            _player.ChangeState("Idle");
    }
}
