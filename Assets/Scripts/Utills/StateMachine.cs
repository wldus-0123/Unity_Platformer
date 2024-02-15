using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<TState, TOwner> where TOwner : Monster
{
    [SerializeField] TOwner owner;
    private Dictionary<TState, IState> states;
    private IState curState;

    public void Update()
    {
        curState.Update();
    }

    public void SetInitState(TState type)
    {
        curState = states[type];
        curState.Enter();
    }

    public void ChangeState(TState type)
    {
        curState.Exit();
        curState = states[type];
        curState.Enter();
    }

    public void AddState(TState type, IState state)
    {
        states.Add(type, state);
    }
}
