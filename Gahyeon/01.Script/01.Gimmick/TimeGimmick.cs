using System;
using UnityEngine;
using UnityEngine.Events;

public class TimeGimmick : MonoBehaviour
{
    private float _remainTime;
    public UnityEvent OnTimeGimmickStart;
    public UnityEvent OnTimeOver;
    public UnityEvent OnTimeGimmickEnd;

    private void OnEnable()
    {
        OnTimeGimmickStart.Invoke();
    }

    private void Update()
    {
        if (_remainTime <= 0)
        {
            OnTimeOver.Invoke();
            return;
        }

        _remainTime -= Time.deltaTime;
    }
    
    public void TimeGimmickEnd()
    {
        OnTimeGimmickEnd?.Invoke();
        this.gameObject.SetActive(false);
    }
    
    public void SetTimer(float time)
    {
        _remainTime = time;
    }
    public void AddTime(float time)
    {
        _remainTime += time;
    }
}