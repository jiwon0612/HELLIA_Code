using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class ToastUI : UIParents
{
    [SerializeField] private string randomText;
    [SerializeField] private int randomTextLength = 3;
    [SerializeField] private float textShowDelay = 1f;
    [SerializeField] private float textShowDuration = 0.75f;
    private Label _toastLabel;

    private bool _isShowing;
    public override void OnEnable()
    {
        base.OnEnable();
        _toastLabel = Root.Query<Label>("ToastLabel");
    }

    // private void Update()
    // {
    //     if (Keyboard.current.spaceKey.wasPressedThisFrame)
    //     {
    //         ShowText("빵닮은꼴보호구역");
    //     }
    // }

    public void ShowText(string text)
    {
        if (_isShowing) return;
        StartCoroutine(ShowTextCoroutine(text));
    }

    private IEnumerator ShowTextCoroutine(string text)
    {
        _isShowing = true;
        _toastLabel.RemoveFromClassList("hide");
        yield return new WaitForSeconds(textShowDelay);
        var textPerSecond = textShowDuration / (text.Length + 1);
        for (var i = 0; i < text.Length; i++)
        {
            if (i >= randomTextLength)
            {
                _toastLabel.text = text[..(i - randomTextLength)];
            }

            _toastLabel.text += randomText[Random.Range(0, randomText.Length)];
            for (var j = 0; j < randomTextLength; j++)
            {
                if (i < text.Length - j + 1 && i > j)
                {
                    _toastLabel.text += randomText[Random.Range(0, randomText.Length)];
                }
            }

            yield return new WaitForSeconds(textPerSecond);
        }

        for (var i = randomTextLength; i > 0; i--)
        {
            _toastLabel.text = text[..(text.Length - i)];
            for (var j = 0; j < i; j++)
            {
                _toastLabel.text += randomText[Random.Range(0, randomText.Length)];
            }

            yield return new WaitForSeconds(textPerSecond);
        }

        _toastLabel.text = text;
        yield return new WaitForSeconds(2f);
        _toastLabel.AddToClassList("hide");
        for (var i = text.Length; i >= 0; i--)
        {
            _toastLabel.text = text[..i];
            yield return new WaitForSeconds(textPerSecond);
        }
        
        _isShowing = false;
    }
}