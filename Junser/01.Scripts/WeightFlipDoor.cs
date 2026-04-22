using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeightFlipDoor : MonoBehaviour, IFragile
{
    [SerializeField]
    private Collider2D _doorCollider;
    [SerializeField]
    private int _weightLimit;
    private int _currentWeight;


    public void AddWeight(int weight)
    {
        _currentWeight += weight;

        if (_currentWeight >= _weightLimit)
        {
            Broke();
        }
    }

    public void Broke()
    {
        _doorCollider.enabled = false;
    }

    public void ReduceWeight(int weight)
    {
        _currentWeight -= weight;
    }
}
