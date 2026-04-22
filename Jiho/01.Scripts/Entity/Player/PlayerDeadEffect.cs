using System;
using UnityEngine;

public class PlayerDeadEffect : MonoBehaviour
{
    public event Action OnDeath;
    public void AnimationEnd()
    {
        OnDeath.Invoke();
        
        OnDeath = null;
        Destroy(gameObject);
    }
}
