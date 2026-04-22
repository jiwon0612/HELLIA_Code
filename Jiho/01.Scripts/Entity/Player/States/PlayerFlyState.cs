using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFlyState : EntityState
{
    private static readonly int BaseColorHash = Shader.PropertyToID("_BlinkColor");
    private Player _player;
    private EntityMovement _movement;
    private AnimationParameterSO _animParameter;

    private SpriteRenderer _flyModeSprite;
    private float _flyDuration;
    private bool _isToughTime;
    private Vector2 _moveVec;

    private Material _mat;
    private Color _defaultColor;
    TrailRenderer _flyTrail;

    public PlayerFlyState(Entity entity, AnimationParameterSO animationParameter) : base(entity, animationParameter)
    {
        _player = entity as Player;
        _movement = entity.GetCompo<EntityMovement>();
        _animParameter = animationParameter;
        _flyModeSprite = _player.transform.Find("FlyMode").GetComponent<SpriteRenderer>();
        _mat = _flyModeSprite.material;
        _defaultColor = _mat.GetColor(BaseColorHash);
        _flyTrail = _flyModeSprite.GetComponent<TrailRenderer>();

        //머리랑 아래 닿는거 안돼 있음
        // _movement.OnWallStateChange += b => _player.ChangeState("Idle");
    }

    public override void Enter()
    {
        base.Enter();
        
        var item = ScriptableObject.CreateInstance<StateSO>();
        item.name = "Fly";

        if (_player.PreviousState.GetType() != typeof(PlayerFlyState))
        {
            //이펙트 생성, 화면 떨림 피드백
            _player.OnDash?.Invoke();
        }

        _movement.IsFly = true;
        
        _movement.SpeedMultiplier = 1.5f;
        _movement.SetGravityScale(0);
        _movement.StopMovement(true);
        
        _flyModeSprite.gameObject.SetActive(true);
        _renderer.gameObject.SetActive(false);
        _renderer.enabled = false;

        _flyDuration = 3.5f;
        
        _moveVec.x = 1;
        if(_player.transform.rotation != Quaternion.identity)
            _moveVec.x = -1;
    }

    public override void Update()
    {
        if(_player.IsDead) return;
        
        base.Update();

        // 주원이가 만들거임
        if(_player.PlayerInput.InputMovement != Vector2.zero) _moveVec = _player.PlayerInput.InputMovement.normalized;
        
        _movement.SetXMovement(_moveVec.x);
        _movement.SetYMovement(_moveVec.y);
        
        _flyDuration -= Time.deltaTime;

        if (_flyDuration <= 0.9f && !_isToughTime)
        {
            _player.StartCoroutine(ToughTime());
            _isToughTime = true;
        }
        
        if (_flyDuration <= 0)
            _player.ChangeState("Idle");
    }

    public override void Exit()
    {
        _movement.IsFly = false;
        
        _movement.StopMovement(true);
        _movement.SpeedMultiplier = 1f;
        _movement.SetGravityScale(1f);

        _flyModeSprite.gameObject.SetActive(false);
        _renderer.gameObject.SetActive(true);
        _renderer.enabled = true;
        
        base.Exit();
    }

    IEnumerator ToughTime()
    {
        yield return new WaitForSeconds(0.1f);
        _mat.SetColor(BaseColorHash, new Color(1, 0f,0f, 1f));
        _flyTrail.material = _mat; 
        
        yield return new WaitForSeconds(0.1f);
        _mat.SetColor(BaseColorHash, _defaultColor);
        _flyTrail.material = _mat; 
        
        _isToughTime = false;
    }
}