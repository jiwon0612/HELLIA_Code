using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : Entity, IInitializable
{
    public UnityEvent OnDash;
    public UnityEvent OnDead;

    [SerializeField] private EntityStatesSO _playerFSM;
    [SerializeField] private PlayerDeadEffect deadEffect;
    [SerializeField] private float dashTime;
    
    [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

    public EntityState CurrentState => _stateMachine.currentState;
    public EntityState PreviousState => _stateMachine.previousState;

    public AnimationParameterSO HitGroundParameter;
    
    public PlayerSoundDataSO soundData;

    public bool IsDead;

    public float jumpPower;
    public float maxJumpPower;
    public float dashSpeed;
    public float dashDuration;

    private readonly float coyoteTime = 0.2f;
    public float CurrentCoyoteTime { get; set; } = 0;

    public readonly float jumpBuffer = 0.12f;
    public float CurrentBufferTime { get; set; } = 0;

    public int dashCount;
    public int jumpCount;

    private int _currentJumpCount;
    private int _currentDashCount;

    private EntityMovement _movement;
    private StateMachine _stateMachine;
    private ParticleSystem _jumpEffect;
    private Collider2D _collider;

    protected override void AfterInitialize()
    {
        base.AfterInitialize();

        _movement = GetCompo<EntityMovement>();

        _stateMachine = new StateMachine(_playerFSM, this);

        _movement.OnGroundState += HandleGroundStateChange;

        PlayerInput.JumpEvent += HandleJumpEvent;
        PlayerInput.DashEvent += HandleDashEvent;
        PlayerInput.CatchEvent += HandleCatchEvent;

        GetCompo<EntityAnimator>().OnAnimationEnd += HandleAnimationEnd;
        IsDead = false;
    }

    private void OnDestroy()
    {
        _movement.OnGroundState -= HandleGroundStateChange;

        PlayerInput.JumpEvent -= HandleJumpEvent;
        PlayerInput.DashEvent -= HandleDashEvent;
        PlayerInput.CatchEvent -= HandleCatchEvent;

        GetCompo<EntityAnimator>().OnAnimationEnd -= HandleAnimationEnd;
    }

    protected override void Awake()
    {
        base.Awake();
        
        _collider = GetComponent<Collider2D>();
        _jumpEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Start() => _stateMachine.Initialize("Idle");

    private void Update()
    {
        _stateMachine.currentState.Update();

        CurrentCoyoteTime -= Time.deltaTime;
    }

    public EntityState GetState(StateSO state) => _stateMachine.GetState(state.stateName);

    public void ChangeState(string newState) => _stateMachine.ChangeState(newState);

    public void ResetDashCount() => _currentDashCount = dashCount;

    public void ResetJumpCount() => _currentJumpCount = jumpCount;

    private void ResetCoyoteTime() => CurrentCoyoteTime = coyoteTime;

    private void HandleAnimationEnd() => CurrentState.AnimationEndTrigger();

    private void HandleDashEvent()
    {
        if (_currentDashCount > 0 && CurrentState != PreviousState) // 딜레이
        {
            _currentDashCount--;
            soundData.PlaySound(SoundType.PlayerDash);
            ChangeState("Dash");
        }
    }

    private void HandleJumpEvent()
    {
        if (_currentJumpCount <= 0 || !_movement.IsGround)
            return;

        _jumpEffect.Play();
        _currentJumpCount--;
        soundData.PlaySound(SoundType.PlayerJump);
        ChangeState("Jump");
    }

    private void HandleCatchEvent()
    {
        if (_movement.IsWall)
            ChangeState("WallIdle");
    }

    private void HandleGroundStateChange(bool isGround)
    {
        if (!isGround || _movement.Velocity.y > 0)
            return;

        if (CurrentBufferTime > 0)
        {
            CurrentBufferTime = 0;
            ChangeState("Jump");
        }

        ResetDashCount();
        ResetJumpCount();
        ResetCoyoteTime();
        _jumpEffect.Stop();
    }

    public void OnDeadEvent()
    {
        OnDead?.Invoke();
    }

    public void Dead()
    {
        if (IsDead)
            return;
        
        _movement._rigid.bodyType = RigidbodyType2D.Static;
        
        _collider.isTrigger = true;
        _movement.StopMovement(true);
        _movement.SetGravityScale(0);
        var obj = Instantiate(deadEffect, transform.position, Quaternion.identity);
        obj.OnDeath += OnDeadEvent;
        IsDead = true;
        soundData.PlaySound(SoundType.PlayerDeath);

        PlayerInput.SetAction(false);
        GetCompo<EntityRenderer>().EntitySprite.enabled = false;
        transform.Find("FlyMode").gameObject.SetActive(false);
    }

    public void Initialize()
    {
        _movement._rigid.bodyType = RigidbodyType2D.Dynamic;
        _collider.isTrigger = false;
        IsDead = false;
        PlayerInput.SetAction(true);
        _movement.SetGravityScale(1);
        GetCompo<EntityRenderer>().EntitySprite.enabled = true;
        ChangeState("Idle");
    }
}