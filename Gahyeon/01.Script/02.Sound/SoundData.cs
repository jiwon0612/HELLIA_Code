using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundData : ScriptableObject
{
    protected abstract AudioClip GetClip(SoundType type);
    public abstract void PlaySound(SoundType type);
}
public enum SoundType
{
    //Same order as the Notion 
    //==========UI==========
    UIButtonClick,
    UIConfirm,
    UICancel,
    UISliderChange,
    UITransition,
    //==========Player==========
    PlayerJump,
    PlayerDash,
    PlayerClimb,
    PlayerDeath,
    //==========Platform==========
    PlatformStepped,
    PlatformBreak,
    PlatformOnOffSwitch,
    PlatformFlip,
    //==========Gimmick==========
    WingCollect,
    WingUsing,
    WeightCollision,
    MirrorMove,
    MirrorInteract,
    DashCharge,
    SightDarken,
    Tracking,
    Transition,
    //==========Background==========
    TitleBGM,
    InGameBGM,
    ClearBGM
}