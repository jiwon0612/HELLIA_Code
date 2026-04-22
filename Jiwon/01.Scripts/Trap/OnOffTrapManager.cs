using UnityEngine;
using System;
using UnityEngine.InputSystem;

public enum OnOffTrapType
{
    On,
    Off
}

public class OnOffTrapManager : MonoBehaviour
{
    [Header("Settings")]
    public Sprite onSprite;
    public Sprite offSprite;
    public LayerMask whatIsTarget;
    [SerializeField] private float switchTime;
    
    public static OnOffTrapManager Instance;
    
    public Action<bool> OnTrapAction;
    
    private float _trapTimer;
    private bool _isCurrentOn;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _trapTimer = switchTime;
    }

    private void Update()
    {
        _trapTimer -= Time.deltaTime;
        if (_trapTimer <= 0)
        {
            _trapTimer = switchTime;
            SetTrap();
        }
    }

    public void SetTrap()
    {
        OnTrapAction?.Invoke(_isCurrentOn);
        _isCurrentOn = !_isCurrentOn;
    }
}
