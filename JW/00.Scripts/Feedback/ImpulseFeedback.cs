using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(CinemachineImpulseSource), typeof(FeedbackPlayer))]
public class ImpulseFeedback : MonoBehaviour, IFeedback
{
    [SerializeField] float _impulseForce;
    private CinemachineImpulseSource _impulseSource;

    private void Awake()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void PlayFeedback()
    {
        _impulseSource.GenerateImpulse(_impulseForce);
    }
}
