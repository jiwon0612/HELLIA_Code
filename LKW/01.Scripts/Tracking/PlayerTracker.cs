using System;
using UnityEngine;

public class PlayerTracker : MonoBehaviour,IInitializable
{
    [SerializeField] private LayerMask _whatIsPlayer;
    private readonly float _showDelay = 0.14f;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private SpriteRenderer _ghostSpriteRenderer;
    private GameObject _ghost;
    [SerializeField] private float _currentTime;
    private bool isStop = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _ghost = transform.Find("Ghost").gameObject;
        _ghostSpriteRenderer = _ghost.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _currentTime =_showDelay;
    }

    private void FixedUpdate()
    {
        ApplyAfterImage();
    }

    // 따라오는 적들에게 위치값과 회전값과 strite 를 적용시켜줄 함수
    public void SetPosAndSprite(TrackingData trackingData)
    {
        if (isStop) return;
        transform.position = trackingData.posData;
        transform.rotation = Quaternion.Euler(trackingData.rotData);
        _spriteRenderer.sprite = trackingData.spriteData;
    }
    
    //간단한 잔상을 보여줄 함수
    private void ApplyAfterImage()
    {
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            GameObject currentGhost = Instantiate(_ghost,transform.position,transform.rotation);
            _ghost.SetActive(true);
            _ghostSpriteRenderer.sprite = _spriteRenderer.sprite;
            Destroy(currentGhost,0.18f);
            _currentTime = _showDelay;
        }
    }

    public void StopTracking()
    {
        isStop = true;
        _animator.enabled = true;
        gameObject.SetActive(false);
    }

    public void StartTracking()
    {
        transform.position = StageManager.instance.checkPoints[StageManager
            .instance.CurrentStageIdx].transform.position + new Vector3(0f,10f,0f);
        isStop = false;
        _animator.enabled = false;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isStop)return;
        if (((1 << collision.gameObject.layer) & _whatIsPlayer) != 0)
        {
            StopTracking();
            if (collision.TryGetComponent(out Player player))
            {
                player.Dead();
            }
        }
    }

    public void Initialize()
    {
        StartTracking();
    }
}