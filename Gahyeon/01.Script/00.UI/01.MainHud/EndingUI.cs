using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndingUI : UIParents
{
    [SerializeField] private TransitionUI transitionUI;
    private VisualElement _container;
    private Button _quitButton;
    public override void OnEnable()
    {
        base.OnEnable();
        _container = Root.Q<VisualElement>("Container");
        _quitButton = Root.Q<Button>("QuitButton");
        
        _quitButton.clicked += () =>
        {
            transitionUI.EnableUI(() =>
            {
                Application.Quit();
            });
        };
    }

    public void ShowEndingUI()
    {
        _container.RemoveFromClassList("hide");
        Time.timeScale = 0;
    }

    public void HideEndingUI()
    {
        _container.AddToClassList("hide");
    }
}
