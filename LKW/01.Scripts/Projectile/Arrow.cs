using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : Projectile
{
    [SerializeField] private float speed;
    protected override void MoveProjectile()
    {
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void ContactPlayer()
    {
        base.ContactPlayer();
    }
}
