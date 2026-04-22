using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

[CreateAssetMenu(fileName = "GimmickSoundData", menuName = "SO/Sound/GimmickSound")]
public class GimmickSoundDataSO : SoundData
{
    public AudioClip WingCollect;
    public AudioClip WingUsing;
    public AudioClip WeightCollision;
    public AudioClip MirrorMove;
    public AudioClip MirrorInteract;
    public AudioClip DashCharge;
    public AudioClip SightDarken;
    public AudioClip Tracking;
    public AudioClip Transition;
    
    protected override AudioClip GetClip(SoundType type)
    {
        switch (type)
        {
            case SoundType.WingCollect:
                return WingCollect;
            case SoundType.WingUsing:
                return WingUsing;
            case SoundType.WeightCollision:
                return WeightCollision;
            case SoundType.MirrorMove:
                return MirrorMove;
            case SoundType.MirrorInteract:
                return MirrorInteract;
            case SoundType.DashCharge:
                return DashCharge;
            case SoundType.SightDarken:
                return SightDarken;
            case SoundType.Tracking:
                return Tracking;
            case SoundType.Transition:
                return Transition;
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
