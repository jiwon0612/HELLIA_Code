using System;
using UnityEngine;
using DG.Tweening;

public class OnOffTrap : MonoBehaviour
{
    [SerializeField] private bool onOffType;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private Sprite _onSprite;
    private Sprite _offSprite;
    private LayerMask _whatIsTarget;

    private bool _currentOn;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        OnOffTrapManager.Instance.OnTrapAction += SetOnOffTrap;
        _onSprite = OnOffTrapManager.Instance.onSprite;
        _offSprite = OnOffTrapManager.Instance.offSprite;
        _whatIsTarget = OnOffTrapManager.Instance.whatIsTarget;
        
    }

    private void OnDisable()
    {
        OnOffTrapManager.Instance.OnTrapAction -= SetOnOffTrap;
    }

    public void SetOnOffTrap(bool on)
    {
        if (onOffType == on)
        {
            _currentOn = true;
            _spriteRenderer.sprite = _onSprite;
        }
        else if (onOffType == !on)
        {
            _currentOn = false;
            _spriteRenderer.sprite = _offSprite;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (_currentOn)
        {
            if (((1 << other.gameObject.layer) & _whatIsTarget) != 0)
            {
                Debug.Log("죽어");
            }
        }
    }
}