using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] BackgroundSoundDataSO backgroundSoundDataSO;
    
    public SoundType soundType;

    private void Start()
    {
        backgroundSoundDataSO.PlaySound(soundType);
    }
}
