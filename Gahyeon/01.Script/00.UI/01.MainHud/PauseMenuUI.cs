using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PauseMenuUI : UIParents
{
    [SerializeField] private Player _player;
    [SerializeField] private TransitionUI _transitionUI;
    [SerializeField] private SettingUI _settingUI;
    [SerializeField] private UIInputSO _uiInput;
    [SerializeField] private int _titleIndex;
    private VisualElement _container;
    private List<Button> _buttons;

    public UnityEvent OnRetryEvent;

    public override void OnEnable()
    {
        base.OnEnable();
        _container = Root.Query<VisualElement>("Container");
        _buttons = Root.Query<Button>().ToList();

        foreach (var button in _buttons)
        {
            button.RegisterCallback<ClickEvent>(OnButtonClick);
        }

        _uiInput.EscapeEvent += ToggleShowHide;
    }

    private void ToggleShowHide()
    {
        if (_transitionUI.IsTransitioning)
        {
            Debug.Log("fuck ou");
            return;
        }
        if (_player != null)
        {
            if (_player.IsDead)
            {
                Debug.Log("fuck ou");
                return;
            }
        }

        
        if (_container.ClassListContains("hide"))
        {
            Debug.Log("shoing");
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void OnButtonClick(ClickEvent evt)
    {
        var index = _buttons.IndexOf(evt.target as Button);
        switch (index)
        {
            case 0:
                Hide();
                break;
            case 1:
                Time.timeScale = 1;
                OnRetryEvent?.Invoke();
                Hide();
                break;
            case 2:
                _transitionUI.EnableUI(_settingUI.Show);
                break;
            case 3:
                _transitionUI.EnableUI(Application.Quit);
                break;
        }
    }

    public override void Show()
    {
        base.Show();
        Time.timeScale = 0;
        _container.RemoveFromClassList("hide");
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1;
        _container.AddToClassList("hide");
    }
}