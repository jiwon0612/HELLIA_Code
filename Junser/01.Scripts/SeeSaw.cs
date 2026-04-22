using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSaw : MonoBehaviour, IInitializable
{
    private Vector2 originPos;
    private Quaternion originRotation;

    private void Start()
    {
        originPos = transform.position;
        originRotation = transform.rotation;
    }
    public void Initialize()
    {
        transform.rotation = originRotation;
        transform.position = originPos;
    }
}
