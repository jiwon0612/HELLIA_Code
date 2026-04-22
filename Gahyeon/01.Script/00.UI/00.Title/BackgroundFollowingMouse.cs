using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowingMouse : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Camera _mainCam;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _mainCam = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition) * _speed;
        _spriteRenderer.material.mainTextureOffset = new Vector2(mousePos.x, Mathf.Clamp(mousePos.y, -0.0625f, 0.0625f));
    }
}
