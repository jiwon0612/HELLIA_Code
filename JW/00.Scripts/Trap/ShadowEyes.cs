using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class ShadowEyes : MonoBehaviour, IInitializable
{
    [SerializeField] private Color shadowBackgroundColor;
    [SerializeField] private Color defaultBackgroundColor;

    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D playerLight;
    [SerializeField] private GimmickSoundDataSO gimmickSoundDataSO;
    
    private bool _isTrigger;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.I))
        {
            Shadow();
        }
#endif
    }

    public void OnShadow()
    {
        if(!_isTrigger)
            gimmickSoundDataSO.PlaySound(SoundType.SightDarken);
        _isTrigger = true;
        
        DOTween.To(() => Camera.main.backgroundColor, x => Camera.main.backgroundColor = x, shadowBackgroundColor, 0.5f);
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 0, 0.5f);
        playerLight.gameObject.SetActive(true);
    }

    public void OutShadow()
    {
        if(_isTrigger)
            gimmickSoundDataSO.PlaySound(SoundType.SightDarken);
        _isTrigger = false;
        
        DOTween.To(() => Camera.main.backgroundColor, x => Camera.main.backgroundColor = x, defaultBackgroundColor, 0.5f);
        DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 1, 0.5f);
        playerLight.gameObject.SetActive(false);
    }

    public void Shadow()
    {
        if (_isTrigger)
        {
            DOTween.To(() => Camera.main.backgroundColor, x => Camera.main.backgroundColor = x, defaultBackgroundColor, 0.5f);
            DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 1, 0.5f);

            playerLight.gameObject.SetActive(false);
            _isTrigger = false;
        }
        else
        {
            DOTween.To(() => Camera.main.backgroundColor, x => Camera.main.backgroundColor = x, shadowBackgroundColor, 0.5f);
            DOTween.To(() => globalLight.intensity, x => globalLight.intensity = x, 0, 0.5f);
            playerLight.gameObject.SetActive(true);
            _isTrigger = true;
        }

        gimmickSoundDataSO.PlaySound(SoundType.SightDarken);
    }

    public void Initialize()
    {
        Camera.main.backgroundColor = defaultBackgroundColor;
        globalLight.intensity = 1;
        
        playerLight.gameObject.SetActive(false);
        _isTrigger = false;
    }
}