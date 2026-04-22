
public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.PlayerInput.JumpEvent += HandleJump;
    }

    public override void Update()
    {
        base.Update();

        if (_movement.IsGround)
            _player.ChangeState("Idle");
    }

    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= HandleJump;
        
        base.Exit();
    }

    private void HandleJump()
    {
        if (!(_player.CurrentCoyoteTime > 0) || _player.PreviousState is PlayerJumpState)
            return;
        
        _player.ChangeState("Jump");
        _player.CurrentCoyoteTime = 0;
    }
}
