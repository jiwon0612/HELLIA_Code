using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Projectile : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2d;
    protected Collider2D _collider2d;
    
    private readonly LayerMask _whatIsPlayer = LayerMask.GetMask("Player");
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 < collision.gameObject.layer) & _whatIsPlayer != 0)
        {
            ContactPlayer();
        }
    }
    
    protected abstract void MoveProjectile();

    protected virtual void ContactPlayer()
    {
        // 비활성화 후 풀에 들어감
    }
}
