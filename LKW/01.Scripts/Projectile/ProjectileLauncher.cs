using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum ProjectileType
{
    arrow
}

public enum ShootDirection
{
    up,
    down,
    right,
    left
}
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private ProjectileType _projectile;
    [SerializeField] private float _fireDelay = 1.5f;
    [SerializeField] private float _lifeTime = 4;

    private Transform _firePos;

    private void Awake()
    {
        _firePos = transform.Find("FirePos").transform;
    }

    private void OnEnable()
    {
        StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(_fireDelay);
        }
    }

    private void Shoot()
    {
        //풀에서 꺼내고 위치를 _firepos로 위치잡기
        
    }
}
