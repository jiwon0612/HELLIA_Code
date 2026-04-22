using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveSquare : MonoBehaviour
{
    private float _moveDirX;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveDirX = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_moveDirX* 4, _rb.velocity.y);
        
    }
}
