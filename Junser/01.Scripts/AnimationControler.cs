using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationControler : MonoBehaviour
{
    public UnityEvent OnBroke;
    public UnityEvent OnEnd;
    [SerializeField] private PlatformSoundDataSO sO;
    [SerializeField] SoundType soundType;

    public void OnBrokeInvoke()
    {
        OnBroke.Invoke();
        sO.PlaySound(soundType);
    }

    public void OnEndInvoke()
    {
        OnEnd.Invoke();
    }
}
