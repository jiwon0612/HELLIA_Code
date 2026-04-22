using Hellmade.Sound;
using UnityEngine;

[CreateAssetMenu(fileName = "UISoundData", menuName = "SO/Sound/UISound")]
public class UISoundDataSO : SoundData
{
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _confirm;
    [SerializeField] private AudioClip _cancel;
    [SerializeField] private AudioClip _sliderChange;
    [SerializeField] private AudioClip _transition;

    protected override AudioClip GetClip(SoundType type)
    {
        switch (type)
        {
            case SoundType.UIButtonClick:
                return _buttonClick;
            case SoundType.UIConfirm:
                return _confirm;
            case SoundType.UICancel:
                return _cancel;
            case SoundType.UISliderChange:
                return _sliderChange;
            case SoundType.UITransition:
                return _transition;
        }
        return null;
    }
    public override void PlaySound(SoundType type)
    {
        var clip = GetClip(type);
        if (clip == null) return;
        EazySoundManager.PlayUISound(clip);
    }
}

