using System;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<CameraData> cameraDatas;
    [SerializeField] private Transform target; // 추적할 대상
    private CinemachineBrain _cinemaBrain;
    private CinemachineBrain _brainCinema;
    private Dictionary<CinemachineVirtualCamera, Vector3> defaultCameraPositions; // 각 카메라의 초기 위치 저장
    public Action OnChangeCamera;

    private float _blendTime;
    public float BlendTime
    {
        get => _blendTime;
        set
        {
            _blendTime = value;
            _cinemaBrain.m_DefaultBlend.m_Time = _blendTime;
        }
    }

    
    private void Awake()
    {
        _brainCinema = FindAnyObjectByType<CinemachineBrain>();
        _cinemaBrain = FindAnyObjectByType<CinemachineBrain>();
        cameraDatas.ForEach(data => data.item.Initialize(target, data.startPos.position, data.endPos.position, data.cameraSize));
    }

    private void OnDrawGizmos()
    {
        foreach (var item in cameraDatas)
        {
            if(!item.startPos && !item.endPos) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(item.startPos.position, item.endPos.position);
            Gizmos.color = Color.white;
        }
    }
}

[Serializable]
public struct CameraData
{
    public CameraItem item;
    public float cameraSize;
    public Transform startPos;
    public Transform endPos;
}