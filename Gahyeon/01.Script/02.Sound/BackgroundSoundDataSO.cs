using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundSoundData", menuName = "SO/Sound/BackgroundSound")]
public class BackgroundSoundDataSO : SoundData
{
    public float DefaultVolume = 0.5f;
    public AudioClip TitleBGM;
    public AudioClip InGameBGM;
    public AudioClip ClearBGM;
    
    protected override AudioClip GetClip(SoundType type)
    {
        switch (type)
        {
            case SoundType.TitleBGM:
                return TitleBGM;
            case SoundType.InGameBGM:
                return InGameBGM;
            case SoundType.ClearBGM:
                return ClearBGM;
        }
        return null;
    }

    public override void PlaySound(SoundType type)
    {
        var clip = GetClip(type);
        if (clip == null) return;
        EazySoundManager.PlayMusic(clip, DefaultVolume, true, false);
    }
    public void PlaySound(SoundType type, float volume)
    {
        var clip = GetClip(type);
        if (clip == null) return;
        EazySoundManager.PlayMusic(clip, volume, true, false);
    }
}