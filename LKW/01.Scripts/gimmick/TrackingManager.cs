using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingManager : MonoBehaviour
{
    [SerializeField] private PlayerCapture _playerCapture;
    [SerializeField] private GameObject _playerTracker;

    public void EnableTracking()
    {
        _playerCapture.enabled = true;
    }

    public void disableTracking()
    {
        _playerCapture.enabled = false;
        _playerTracker.SetActive(false);
    }
}
