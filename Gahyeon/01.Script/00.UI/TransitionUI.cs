using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class TransitionUI : UIParents
{
    [SerializeField] private UISoundDataSO soundData;
    [SerializeField] private bool showTransitionOnStart = true;
    private List<VisualElement> _boxes;

    [FormerlySerializedAs("BetweenTransitionsAction")]
    public UnityEvent OnDeadTransition;

    public static bool HaveBeenPlayed { get; private set; } = false;

    public bool IsTransitioning { get; private set; } = false;

    public override void OnEnable()
    {
        base.OnEnable();
        _boxes = Root.Query<VisualElement>("Box").ToList();
    }

    private void Start()
    {
        if (HaveBeenPlayed)
        {
            showTransitionOnStart = true;
        }

        if (showTransitionOnStart)
        {
            StartCoroutine(HideBoxesOnStart());
        }
    }

    private IEnumerator HideBoxesOnStart()
    {
        StartCoroutine(ShowBoxes());
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(HideBoxes());
    }

    public void EnableUI(Action action = null)
    {
        StartCoroutine(ShowBoxes(action));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ShowBoxes(Action action = null)
    {
        if (IsTransitioning)
        {
            yield break;
        }

        IsTransitioning = true;
        foreach (var box in _boxes)
        {
            box.RemoveFromClassList("hide");
        }

        yield return new WaitForSecondsRealtime(1.5f);
        action?.Invoke();
        IsTransitioning = false;
    }

    public void DisableUI(Action action = null)
    {
        StartCoroutine(HideBoxes(action));
    }

    private IEnumerator HideBoxes(Action action = null)
    {
        soundData.PlaySound(SoundType.UITransition);
        foreach (var box in _boxes)
        {
            box.AddToClassList("hide");
        }

        yield return new WaitForSecondsRealtime(1.5f);
        action?.Invoke();
    }

    public void TriggerFadeInOut(Action action) => EnableUI(() =>
    {
        action();
        DisableUI();
    });

    public void TriggerFadeInOut() => EnableUI(() =>
    {
        OnDeadTransition?.Invoke();
        DisableUI();
    });

    public void ChangeScene(int index)
    {
        HaveBeenPlayed = true;
        EnableUI(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        });
    }
}