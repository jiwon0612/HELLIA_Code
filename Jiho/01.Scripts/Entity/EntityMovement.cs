using System;
using UnityEngine;

public class EntityMovement : MonoBehaviour, IEntityComponent
{
    [Header("[ move value ]")] [SerializeField]
    private AnimationParameterSO _ySpeedParameter;

    [SerializeField] private float moveSpeed = 10f;

    [Header("[ ground ]")] [SerializeField]
    private Transform groundTrm;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 groundCheckSize;

    [Header("[ wall ]")] [SerializeField] private Transform wallTrm;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private Vector2 wallCheckSize;

    [SerializeField] private PlayerInputSO input;

    public Action<bool> OnGroundState;

    public Vector2 Velocity => _rigid.velocity;

    public bool IsGround { get; private set; }
    public bool IsWall { get; private set; }
    public bool IsCatch { get; private set; }
    public bool IsFly { get; set; }
    public bool CanManualMove { get; set; } = true;

    public float SpeedMultiplier { get; set; } = 1f;

    private Entity _entity;
    private EntityRenderer _renderer;
    public Rigidbody2D _rigid;

    private float _defaultGravityScale;
    private float _xMovement;
    private float _yMovement;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _renderer = _entity.GetCompo<EntityRenderer>();
        _rigid = _entity.GetComponent<Rigidbody2D>();

        _defaultGravityScale = _rigid.gravityScale;
    }

    public void SetGravityScale(float value)
        => _rigid.gravityScale = _defaultGravityScale * value;

    public void AddForce(Vector2 force, ForceMode2D forceMode = ForceMode2D.Impulse)
        => _rigid.AddForce(force, forceMode);

    public void StopMovement(bool isYStop = false)
    {
        if (isYStop)
        {
            _rigid.velocity = Vector2.zero;
            _yMovement = 0;
        }
        else
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);

        _xMovement = 0;
    }

    public void SetXMovement(float xMovement) => _xMovement = xMovement;

    public void SetYMovement(float yMovement) => _yMovement = yMovement;

    private void FixedUpdate()
    {
        CheckGround();
        CheckWall();
        CheckCatch();
        MoveCharacter();
    }

    private void CheckGround()
    {
        var before = IsGround;

        IsGround = Physics2D.OverlapBox(groundTrm.position, groundCheckSize,
            0, whatIsGround);

        OnGroundState?.Invoke(IsGround);
    }

    private void CheckWall()
    {
        IsWall = Physics2D.OverlapBox(wallTrm.position, wallCheckSize,
            0, whatIsWall);
    }

    private void CheckCatch()
    {
        if (!IsWall)
        {
            IsCatch = false;
            return;
        }

        IsCatch = input.isCatchKeyPressed;
    }

    private void MoveCharacter()
    {
        if (CanManualMove)
        {
            var xSpeed = _xMovement * moveSpeed * SpeedMultiplier;

            if (IsCatch || IsFly)
            {
                var ySpeed = _yMovement * moveSpeed * SpeedMultiplier;

                _rigid.velocity = new Vector2(xSpeed, ySpeed);
            }
            else
                _rigid.velocity = new Vector2(xSpeed, _rigid.velocity.y);

            _renderer.FlipController(_xMovement);
        }

        _renderer.SetParameter(_ySpeedParameter, _rigid.velocity.y);
    }

    private void OnDrawGizmos()
    {
        #region ground gizmos

        if (groundTrm == null)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundTrm.position, groundCheckSize);

        #endregion

        #region wall gizmos

        if (wallTrm == null)
            return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(wallTrm.position, wallCheckSize);

        #endregion

        Gizmos.color = Color.white;
    }
}