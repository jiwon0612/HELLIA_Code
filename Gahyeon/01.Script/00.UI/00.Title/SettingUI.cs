using System.Collections;
using System.Collections.Generic;
using System.IO;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SettingUI : UIParents
{
    private const int DefaultSliderValue = 100;
    private const float HideDelay = 0.5f;

    [SerializeField] private TransitionUI _transitionUI;
    [SerializeField] PlayerInputSO _playerInput;
    [SerializeField] UIInputSO _uiInput;
    private Controls _controls => _playerInput._controls;

    private string _soundPathName = "soundValues.json";
    private string SoundPath => Path.Combine(Application.persistentDataPath, _soundPathName);
    private string _prevBinding;

    private VisualElement _container;
    private VisualElement _hider;

    private List<SliderInt> _sliders;
    private int[] _sliderValues = new int[4];

    List<KeyBindingButton> _keyBindingButtons;

    private Button _confirmButton;
    private Button _cancelButton;

    public override void OnEnable()
    {
        base.OnEnable();
        InitializeUIElements();
        LoadSoundValues();
    }

    private void InitializeUIElements()
    {
        _container = Root.Query<VisualElement>("Container");
        _hider = Root.Query<VisualElement>("Hider");

        _sliders = Root.Query<SliderInt>().ToList();
        foreach (var slider in _sliders)
        {
            slider.RegisterValueChangedCallback(OnSliderValueChanged);
        }

        _keyBindingButtons = Root.Query<KeyBindingButton>().ToList();
        foreach (var button in _keyBindingButtons)
        {
            button.InitializeKeyBindingButton(_controls);
        }
        _prevBinding = _controls.SaveBindingOverridesAsJson();

        _confirmButton = Root.Query<Button>("ConfirmButton");
        _cancelButton = Root.Query<Button>("CancelButton");
        _confirmButton.clicked += OnConfirmed;
        _cancelButton.clicked += OnCanceled;
    }

    private void LoadSoundValues()
    {
        if (File.Exists(SoundPath))
        {
            var soundValues = JsonUtility.FromJson<SoundValues>(File.ReadAllText(SoundPath));
            for (var i = 0; i < _sliders.Count; i++)
            {
                _sliders[i].value = soundValues.Values[i];
                _sliderValues[i] = soundValues.Values[i];
            }
        }
        else
        {
            SetDefaultSliderValues();
        }
    }

    private void SetDefaultSliderValues()
    {
        for (var i = 0; i < _sliders.Count; i++)
        {
            _sliders[i].value = DefaultSliderValue;
            _sliderValues[i] = DefaultSliderValue;
        }
    }

    public override void Show()
    {
        _container.RemoveFromClassList("hide");
        _hider.pickingMode = PickingMode.Ignore;
        _uiInput.EscapeEvent += OnConfirmed;
    }

    public override void Hide()
    {
        _container.AddToClassList("hide");
        _hider.pickingMode = PickingMode.Position;
        _uiInput.EscapeEvent -= OnConfirmed;
        StartCoroutine(HideWithDelay());
    }

    IEnumerator HideWithDelay()
    {
        yield return new WaitForSecondsRealtime(HideDelay);
        _transitionUI.DisableUI();
    }

    private void OnSliderValueChanged(ChangeEvent<int> evt)
    {
        var sliderIndex = _sliders.IndexOf(evt.target as SliderInt);
        _sliderValues[sliderIndex] = evt.newValue;
        UpdateSoundManagerVolume(sliderIndex, evt.newValue);
    }

    private void UpdateSoundManagerVolume(int sliderIndex, int newValue)
    {
        var volume = newValue / 100f;
        switch (sliderIndex)
        {
            case 0:
                EazySoundManager.GlobalVolume = volume;
                break;
            case 1:
                EazySoundManager.GlobalMusicVolume = volume;
                break;
            case 2:
                EazySoundManager.GlobalSoundsVolume = volume;
                break;
            case 3:
                EazySoundManager.GlobalUISoundsVolume = volume;
                break;
        }
    }

    private void OnConfirmed()
    {
        SaveSoundValues();
        _prevBinding = _controls.SaveBindingOverridesAsJson();
        Hide();
    }

    private void SaveSoundValues()
    {
        var soundValues = new SoundValues
        {
            Values = new List<int>(_sliderValues)
        };
        File.WriteAllText(SoundPath, JsonUtility.ToJson(soundValues));
    }

    private void OnCanceled()
    {
        LoadSoundValues();
        ResetKeyBindings();
        Hide();
    }

    private void ResetKeyBindings()
    {
        foreach (var button in _keyBindingButtons)
        {
            button.PreviousKey = _prevBinding;
            button.ResetKeyBinding();
        }
    }
}

public class SoundValues
{
    public List<int> Values;
}