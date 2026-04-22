using UnityEngine;

public class PlayerAirState : EntityState
{
    protected Player _player;
    protected EntityMovement _movement;

    private float _timeInAir;
    private readonly float _gravityDelay = 0.25f;
    
    public PlayerAirState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
        _player = entity as Player;
        _movement = _player.GetCompo<EntityMovement>();
    }
    
    public override void Enter()
    {
        base.Enter();
        
        // 플레이어 공중 이동 속도
        _movement.SpeedMultiplier = 1.25f;
        _player.PlayerInput.JumpEvent += HandleJump;
    }
    
    public override void Update()
    {
        base.Update();

        CalculateJumpBuffer();
        
        if (!_player.IsDead)
        {
            CalculateAirTime();
            ApplyExtraGravity();
        }
        
        var xInput = _player.PlayerInput.InputMovement.x;

        if (Mathf.Abs(xInput) > 0)
            _movement.SetXMovement(xInput);
    }
    
    public override void Exit()
    {
        _movement.SpeedMultiplier = 1f;
        _player.PlayerInput.JumpEvent -= HandleJump;

        base.Exit();
    }
    
    private void CalculateJumpBuffer()
    {
        if (_player.CurrentBufferTime >= 0)
            _player.CurrentBufferTime -= Time.deltaTime;
    }
    
    private void HandleJump()
    {
        _player.CurrentBufferTime = _player.jumpBuffer;
    }

    private void CalculateAirTime()
    {
        if (_movement.IsGround)
            _timeInAir = 0;
        else
            _timeInAir += Time.deltaTime;
    }
    
    private void ApplyExtraGravity()
    {
        if (!(_timeInAir > _gravityDelay))
            return;
        
        _movement.SetGravityScale(2);
        _timeInAir = 0;
    }
}
