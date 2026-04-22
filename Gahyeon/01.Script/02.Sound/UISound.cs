using System;
using System.Collections.Generic;
using System.Linq;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class UISound : MonoBehaviour
{
    [SerializeField] private List<string> confirmButtonNames;
    [SerializeField] private List<string> cancelButtonNames;
    [SerializeField] private UISoundDataSO uiSoundDataSo;

    [SerializeField] private AudioClip bgm;

    private List<UIParents> _uiDocumentsInChildren;

    private void OnEnable()
    {
        _uiDocumentsInChildren = GetComponentsInChildren<UIParents>().ToList();
    }

    private void Start()
    {
        foreach (var ui in _uiDocumentsInChildren)
        {
            foreach (var btn in ui.Root.Query<Button>().ToList())
            {
                if (confirmButtonNames.Contains(btn.name))
                {
                    btn.RegisterCallback<ClickEvent>(PlayConfirmSound);
                }
                else if (cancelButtonNames.Contains(btn.name))
                {
                    btn.RegisterCallback<ClickEvent>(PlayCancelSound);
                }
                else
                {
                    btn.RegisterCallback<ClickEvent>(PlayClickSound);
                }
            }
        }

        if (bgm != null)
        {
            EazySoundManager.PlayMusic(bgm, 0.5f, true, false);
        }
    }

    private void OnDestroy()
    {
        foreach (var ui in _uiDocumentsInChildren)
        {
            foreach (var btn in ui.Root.Query<Button>().ToList())
            {
                if (confirmButtonNames.Contains(btn.name))
                {
                    btn.UnregisterCallback<ClickEvent>(PlayConfirmSound);
                }
                else if (cancelButtonNames.Contains(btn.name))
                {
                    btn.UnregisterCallback<ClickEvent>(PlayCancelSound);
                }
                else
                {
                    btn.UnregisterCallback<ClickEvent>(PlayClickSound);
                }
            }
        }
    }

    private void PlayClickSound(ClickEvent evt)
    {
        uiSoundDataSo.PlaySound(SoundType.UIButtonClick);
    }

    private void PlayConfirmSound(ClickEvent evt)
    {
        uiSoundDataSo.PlaySound(SoundType.UIConfirm);
    }

    private void PlayCancelSound(ClickEvent evt)
    {
        uiSoundDataSo.PlaySound(SoundType.UICancel);
    }
}