using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Animator/Parameter")]
public class AnimationParameterSO : ScriptableObject
{
    public enum ParameterType
    {
        Boolean,
        Float,
        Integer,
        Trigger
    }

    public string parameterName;
    public ParameterType parameterType;
    public int hashValue;
    
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(parameterName))
            return;

        hashValue = Animator.StringToHash(parameterName);
    }
}
