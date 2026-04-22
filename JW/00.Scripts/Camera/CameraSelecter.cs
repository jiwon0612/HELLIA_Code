using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSelecter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera;
    [SerializeField] private GameObject _target;
    [field: SerializeField] public bool IsFollow { get; private set; }

    private CinemachineBrain _brainCinema;
    private CinemachineConfiner2D _confiner;
    private bool _isMoveCamera;
    private Vector3 _cinemaDefaultPos;

    private void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        _brainCinema = FindAnyObjectByType<CinemachineBrain>();
        _confiner = GetComponent<CinemachineConfiner2D>();

        _cinemaDefaultPos = _cinemachineCamera.transform.position;
    }

    private void Update()
    {
        if (_brainCinema.IsBlending)
        {
            //플레이어 정지

            return;
        }

        bool isInCameraView = IsTargetInsideConfiner(_cinemachineCamera.transform.position) &&
                              IsObjectInOrthographicView(_cinemachineCamera, _target.transform.position);
        if (IsFollow && isInCameraView)
        {
            _cinemachineCamera.Priority = 10;
            _cinemachineCamera.Follow = _target.transform;
            return;
        }
        else
        {
            if (IsFollow)
            {
                _cinemachineCamera.Priority = 9;
                _cinemachineCamera.Follow = null;
                _cinemachineCamera.transform.position = _cinemaDefaultPos;
            }
        }

        if (!IsFollow && IsObjectInOrthographicView(_cinemachineCamera, _target.transform.position))
            _cinemachineCamera.Priority = 10;
        else
        {
            if (!IsFollow)_cinemachineCamera.Priority = 9;
        }
    }

    /*private bool IsTargetOutsideViewport()
    {
        var cinemacinePos = Camera.main.transform.position - _cinemachineCamera.transform.position;

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(cinemacinePos + _target.transform.position);
        return viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;
    }*/

    private Vector2 GetScreenSize()
    {
        float height = _cinemachineCamera.m_Lens.OrthographicSize * 2;
        float width = Screen.width / Screen.height * height - _cinemachineCamera.m_Lens.OrthographicSize / 2 + 0.5f;
        return new Vector2(width, height);
    }

    bool IsObjectInOrthographicView(CinemachineVirtualCamera vCam, Vector3 objectPosition)
    {
        float halfHeight = vCam.m_Lens.OrthographicSize;
        float halfWidth = halfHeight * vCam.m_Lens.Aspect;

        // 카메라 중심 좌표 (월드 좌표)
        Vector3 cameraCenter = vCam.transform.position;

        // 오브젝트가 화면 안에 있는지 확인
        return objectPosition.x >= cameraCenter.x - halfWidth &&
               objectPosition.x <= cameraCenter.x + halfWidth &&
               objectPosition.y >= cameraCenter.y - halfHeight &&
               objectPosition.y <= cameraCenter.y + halfHeight;
    }

    public bool IsTargetInsideConfiner(Vector3 targetPosition)
    {
        if (_confiner == null) return false;
        Collider2D boundingShape = _confiner.m_BoundingShape2D;

        // 대상이 Confiner 안에 있는지 확인
        return boundingShape.OverlapPoint(targetPosition);
    }
}