using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasWeight
{
    public void CheckFragileFlatForm(Vector2 rayDir, float checkLength, LayerMask checkingLayer);
}
