using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour,IInitializable
{
    [SerializeField] private Door5 _door;
    private Vector3 _originPos;
    private SpriteRenderer _spriteRenderer;
    private bool _isTriggered = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _originPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTriggered)return;
        if (collision.gameObject.CompareTag("Player"))
        {
            _door.CurrentCount--;
            _spriteRenderer.color = new Color(1, 1, 1, 1);
            _isTriggered = true;
        }
    }


    public void Initialize()
    {
        _isTriggered = false;
        _spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        transform.position = _originPos;
    }
}
