using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCapture : MonoBehaviour,IInitializable
{
    [SerializeField] private float  _chaseDelay = 3;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] PlayerTracker playerTracker;
    private bool _isTrackerEnable = false;

    private Queue<TrackingData> _trackingDataBuffer;
    
    TrackingData _trackingData;
    
    private void Awake()
    {
        _trackingDataBuffer = new Queue<TrackingData>();
        _spriteRenderer =  transform.parent.Find("Visual").GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerTracker.gameObject.SetActive(true);
    }

    private void Update()
    {
        CaptureData();
        ApplyTrackingData(_trackingData);
    }

    private void CaptureData()
    {
        _trackingData.posData = transform.position;
        _trackingData.spriteData = _spriteRenderer.sprite;
        _trackingData.rotData = transform.eulerAngles;
        _trackingDataBuffer.Enqueue(_trackingData);
    }

    private void ApplyTrackingData(TrackingData trackingData)
    {
        _chaseDelay -= Time.deltaTime;
        if (_chaseDelay>0)return;
        if (!_isTrackerEnable)
        {
            playerTracker.gameObject.SetActive(true);
            _isTrackerEnable = true;
        }
        playerTracker.SetPosAndSprite(_trackingDataBuffer.Peek());
        _trackingDataBuffer.Dequeue();
    }

    public void InitBuffer()
    {
        _trackingDataBuffer.Clear();
    }

    public void Initialize()
    {
        _isTrackerEnable = false;
        _chaseDelay = 1;
        InitBuffer();
    }
}