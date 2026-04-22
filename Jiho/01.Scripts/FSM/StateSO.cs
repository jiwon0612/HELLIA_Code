using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FSM/State")]
public class StateSO : ScriptableObject
{
    public string stateName;
    public string className;
    public AnimationParameterSO stateAnimation;
}
