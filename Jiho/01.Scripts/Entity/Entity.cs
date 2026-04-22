                                        using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private Dictionary<Type, IEntityComponent> _componentDictionary = new();

    protected virtual void Awake()
    {
        GetComponentsInChildren<IEntityComponent>(true).ToList()
            .ForEach(component => _componentDictionary.Add(component.GetType(),component));
        
        InitializeComponents();
        AfterInitialize();
    }
    
    private void InitializeComponents()
    {
        _componentDictionary.Values.ToList().ForEach(component => component.Initialize(this));
    }

    protected virtual void AfterInitialize()
    {
        _componentDictionary.Values.ToList().ForEach(component =>
        {
            if (component is IAfterInitable afterInitable)
                afterInitable.AfterInitialize();
        });
    }
    
    public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent
    {
        if (_componentDictionary.TryGetValue(typeof(T), out IEntityComponent entityComponent))
            return (T)entityComponent;

        if (isDerived)
        {
            var findType = _componentDictionary.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));

            if (findType != null)
                return (T)_componentDictionary[findType];
        }

        return default;
    }
}