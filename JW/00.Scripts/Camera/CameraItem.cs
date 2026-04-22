using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraItem : MonoBehaviour, IInitializable
{
    [field: SerializeField] public CinemachineVirtualCamera CinemaCam { get; private set; }
    [SerializeField] private bool isBottomToTop;
    private Transform _target;
    private bool _isHorizontal;

    private Vector2 _startPos;
    private Vector2 _endPos;
    private Vector2 offset;

    private void Awake()
    {
        CinemaCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void Initialize(Transform target, Vector2 startPos, Vector2 endPos, float size)
    {
        _target = target;
        _startPos = startPos;
        _endPos = endPos;
        _isHorizontal = (endPos - startPos).y == 0;

        CinemaCam.m_Lens.OrthographicSize = size;

        offset = new Vector2(0.5f, 0);
    }

    private void Update()
    {
        CinemaCam.Priority = IsObjectInOrthographicView(CinemaCam, _target.position) ? 10 : 9;


        if (CinemaCam.Priority < 10) return;
        
        if (isBottomToTop)
        {
            transform.position = ClampVector(transform.position, _startPos, _endPos);
        }
        else
        {
            transform.position = ClampVector(transform.position, _endPos, _startPos);
        }

        bool isOutLeftToRight = transform.position.x < _startPos.x && transform.position.x > _endPos.x && isBottomToTop && _isHorizontal;
        bool isOutRightToLeft = transform.position.x > _startPos.x && transform.position.x < _endPos.x && !isBottomToTop && _isHorizontal;
        bool isOutBottomToTop = transform.position.y < _startPos.y && transform.position.y > _endPos.y && isBottomToTop && !_isHorizontal;
        bool isOutTopToBottom = transform.position.y > _startPos.y && transform.position.y < _endPos.y && !isBottomToTop && !_isHorizontal;
        
        if(isOutLeftToRight && isOutRightToLeft && isOutBottomToTop && isOutTopToBottom) return;

        if (IsObjectInOrthographicView(CinemaCam, _target.position, _isHorizontal) > 0.9f)
        {
            Vector3 movePos = Vector3.zero;
            
            if (isBottomToTop)
            {
                movePos = ClampVector(_target.position, _startPos, _endPos);
                
                transform.position = new Vector3(movePos.x, movePos.y, -10);
            }
            else
            {
                movePos = ClampVector(_target.position, _endPos, _startPos);
                
                transform.position = new Vector3(movePos.x, movePos.y, -10);
            }

            transform.DOMove(movePos, 1f).SetEase(Ease.Linear);
        }

    }

    private void LateUpdate()
    {
        if (CinemaCam.Priority < 10) return;

        Vector3 targetPosition;
        Vector3 vec = Vector3.zero;
        Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, -10);
        
        if (isBottomToTop)
        {
            targetPosition = ClampVector(_target.position, _startPos, _endPos);
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, -10);
        }
        else
        {
            targetPosition = ClampVector(_target.position, _endPos, _startPos);
            targetPosition = new Vector3(targetPosition.x, targetPosition.y, -10);
        }

        // 카메라를 부드럽게 목표 위치로 이동
        transform.position = Vector3.SmoothDamp(currentPos, targetPosition, ref vec, 0.05f);
    }

    private bool IsObjectInOrthographicView(CinemachineVirtualCamera vCam, Vector3 objectPosition)
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

    private float IsObjectInOrthographicView(CinemachineVirtualCamera vCam, Vector3 objectPosition, bool isHorizontal)
    {
        float halfHeight = vCam.m_Lens.OrthographicSize * 2;
        float halfWidth = halfHeight * vCam.m_Lens.Aspect;

        // 카메라 중심 좌표 (월드 좌표)
        Vector3 cameraCenter = vCam.transform.position;

        if (isHorizontal)
        {
            return Mathf.Abs(objectPosition.x - cameraCenter.x) / halfHeight;
        }
        else
        {
            return Mathf.Abs(objectPosition.y - cameraCenter.y) / halfWidth * 5;
        }
    }

    private Vector2 ClampVector(Vector2 vec, Vector2 min, Vector2 max)
    {
        float xValue = vec.x;
        float yValue = vec.y;
        
        return new Vector2(Mathf.Clamp(xValue, min.x, max.x), Mathf.Clamp(yValue, min.y, max.y));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (!CinemaCam) return;
        var vCam = CinemaCam;

        float halfHeight = vCam.m_Lens.OrthographicSize * 2;
        float halfWidth = halfHeight * vCam.m_Lens.Aspect;

        Vector3 camSize = new Vector3(halfWidth, halfHeight);

        Gizmos.DrawWireCube(vCam.transform.position, camSize);
        Gizmos.color = Color.white;
    }

    public void Initialize()
    {
        transform.position = _startPos;
    }
}