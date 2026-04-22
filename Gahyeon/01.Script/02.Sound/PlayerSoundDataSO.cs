using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSoundData", menuName = "SO/Sound/PlayerSound")]
public class PlayerSoundDataSO : SoundData
{
    public AudioClip PlayerJump;
    public AudioClip PlayerDash;
    public AudioClip PlayerClimb;
    public AudioClip PlayerDeath;
    protected override AudioClip GetClip(SoundType type)
    {
        switch (type)
        {
            case SoundType.PlayerJump:
                return PlayerJump;
            case SoundType.PlayerDash:
                return PlayerDash;
            case SoundType.PlayerClimb:
                return PlayerClimb;
            case SoundType.PlayerDeath:
                return PlayerDeath;
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