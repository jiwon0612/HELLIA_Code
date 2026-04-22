using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeightKeyDoor : MonoBehaviour, IFragile, IInitializable
{
    [SerializeField]
    private Collider2D _doorCollider;
    [SerializeField]
    private int _weightLimit;
    private int _currentWeight;
    [SerializeField]
    private string animationName;
    private int animationHash;
    [SerializeField]
    private PlatformSoundDataSO _soundDataSO;
    private ParticleSystem _particle;
    private Animator _anim;

    private void Awake()
    {
        animationHash = Animator.StringToHash(animationName);
        _anim = _doorCollider.GetComponent<Animator>();
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    public void PlayParticle()
    {
        _particle.Play();
    }

    public void AddWeight(int weight)
    {
        _currentWeight += weight;

        if(_currentWeight >= _weightLimit)
        {
            _anim.SetTrigger(animationHash);
        }
    }

    public void Broke()
    {
        if (_doorCollider == null) return;
        _soundDataSO.PlaySound(SoundType.PlatformBreak);
        _doorCollider.enabled = false;
    }

    public void ReduceWeight(int weight)
    {
        _currentWeight -= weight;
    }

    public void Initialize()
    {
        _anim.SetTrigger(Animator.StringToHash("Rebuild"));
        _doorCollider.enabled = true;
        _currentWeight = 0;
    }
}
