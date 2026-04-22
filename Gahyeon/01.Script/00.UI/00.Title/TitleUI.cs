using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleUI : UIParents
{
    [SerializeField] private UIInputSO uiInput;
    [SerializeField] private TransitionUI transitionUI;
    [SerializeField] private SettingUI settingUI;
    [SerializeField] private int playSceneIndex = 1;
    
    private VisualElement _container;
    private VisualElement _buttons;

    private Label _pressAnyKeyLabel;
    
    private Button _startButton;
    private Button _optionButton;
    private Button _howToPlayButton;
    private Button _exitButton;

    private bool _isButtonsActive = false;

    public override void OnEnable()
    {
        base.OnEnable();
        _container = Root.Query<VisualElement>("Container");
        _buttons = Root.Query<VisualElement>("Buttons");
        
        _startButton = Root.Query<Button>("PlayButton");
        _optionButton = Root.Query<Button>("OptionButton");
        _exitButton = Root.Query<Button>("QuitButton");
        
        _pressAnyKeyLabel = Root.Query<Label>("ClickToStart");
        
        _startButton.clicked += OnStartButtonClicked;
        _optionButton.clicked += OnOptionButtonClicked;
        _exitButton.clicked += OnExitButtonClicked;
    }

    private void OnDisable()
    {
        _startButton.clicked -= OnStartButtonClicked;
        _optionButton.clicked -= OnOptionButtonClicked;
        _exitButton.clicked -= OnExitButtonClicked;
    }


    private void Start()
    {
        if (TransitionUI.HaveBeenPlayed)
        {
            _container.RemoveFromClassList("hide");
            HandleAnyKeyEvent();
            return;
        }
        StartCoroutine(WaitAndShow(_container, 0.1f));
    }

    private void HandleAnyKeyEvent()
    {
        if(_isButtonsActive) return;
        _isButtonsActive = true;
        _container.RemoveFromClassList("wide");
        _pressAnyKeyLabel.AddToClassList("hide");
        _buttons.RemoveFromClassList("hide");
        uiInput.AnyKeyEvent -= HandleAnyKeyEvent;
    }
    private IEnumerator WaitAndShow(VisualElement element, float delay, bool isShow = true)
    {
        yield return new WaitForSeconds(delay);
        if (isShow)
        {
            element.RemoveFromClassList("hide");
            uiInput.AnyKeyEvent += HandleAnyKeyEvent;
        }
        else
        {
            element.AddToClassList("hide");
        }
    }

    private void OnStartButtonClicked()
    {
        _startButton.AddToClassList("clicked");
        transitionUI.ChangeScene(playSceneIndex);
    }
    private void OnOptionButtonClicked()
    {
        _optionButton.AddToClassList("clicked");
        transitionUI.EnableUI(() =>
        {
            settingUI.Show();
            _optionButton.RemoveFromClassList("clicked");
        });
    }

    private void OnExitButtonClicked()
    {
        _exitButton.AddToClassList("clicked");
        transitionUI.EnableUI(Application.Quit);
    }
}
