using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public EntityState currentState { get; private set; }
    public EntityState previousState { get; private set; }

    private Dictionary<string, EntityState> _stateDictionary;

    public StateMachine(EntityStatesSO entityFSM, Entity owner)
    {
        _stateDictionary = new Dictionary<string, EntityState>();

        foreach (StateSO state in entityFSM.states)
        {
            try
            {
                Type type = Type.GetType(state.className);

                EntityState playerState = Activator.CreateInstance(type, owner, state.stateAnimation) as EntityState;
                _stateDictionary.Add(state.stateName, playerState);
            }
            catch (Exception exception)
            {
                Debug.LogError($"{state.className} 를 로드하지 못했습니다. 에러 메시지 : {exception.Message}");
            }
        }
    }
    
    public EntityState GetState(string stateName)
    {
        return _stateDictionary.GetValueOrDefault(stateName);
    }
    
    public void UpdateStateMachine()
    {
        currentState.Update();
    }
    
    public void Initialize(string stateName)
    {
        EntityState startState = GetState(stateName);
        Debug.Assert(startState != null, $"시작 상태({startState})가 null입니다.");
        
        currentState = startState;
        currentState.Enter();
    }
    
    public void ChangeState(string newState)
    {
        currentState.Exit();

        EntityState nextState = GetState(newState);
        Debug.Assert(nextState != null, $"다음 상태({newState})를 찾지 못했습니다.");
        
        previousState = currentState;
        currentState = nextState;
        currentState.Enter();
    }
}
