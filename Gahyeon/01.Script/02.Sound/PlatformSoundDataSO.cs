using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformSoundData", menuName = "SO/Sound/PlatformSound")]
public class PlatformSoundDataSO : SoundData
{
    public AudioClip PlatformStepped;
    public AudioClip PlatformBreak;
    public AudioClip PlatformOnOffSwitch;
    public AudioClip PlatformFlip;
    
    protected override AudioClip GetClip(SoundType type)
    {
        switch (type)
        {
            case SoundType.PlatformStepped:
                return PlatformStepped;
            case SoundType.PlatformBreak:
                return PlatformBreak;
            case SoundType.PlatformOnOffSwitch:
                return PlatformOnOffSwitch;
            case SoundType.PlatformFlip:
                return PlatformFlip;
        }
        return null;
    }

    public override void PlaySound(SoundType type)
    {
        var clip = GetClip(type);
        if (clip == null) return;
        EazySoundManager.PlaySound(clip);
    }
}
