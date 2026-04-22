using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FragileFlatForm : MonoBehaviour, IFragile
{
    private Collider2D _collider;

    [SerializeField] private float weightLimit;
    [SerializeField] private PlatformSoundDataSO _soundDataSO;

    public float currentWeight = 0;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    public void AddWeight(int weight)
    {
        if (_collider == null) return;

        currentWeight += weight;

        if (currentWeight >= weightLimit)
        {
            Broke();
        }
    }

    public void Broke()
    {
        _soundDataSO.PlaySound(SoundType.PlatformBreak);
        _collider.enabled = false;
    }

    public void ReduceWeight(int weight)
    {
        if (_collider == null) return;

        currentWeight -= weight;

        if (currentWeight >= weightLimit)
        {
            Broke();
        }
    }
}
