using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State Machine", menuName = "States/State Machine")]
public class StateMachine : ScriptableObject
{
    [Header("Parameters")]
    [SerializeField] State _currentState;
    [SerializeField] State _testState;

    public State currentState => _currentState;
    public State testState => _testState;

    public void SetState(State p_state)
    {
        if (_currentState == p_state) return;
        if (_currentState != null) _currentState.ExitState();
        _currentState = p_state;
        _currentState.EnterState();
        return;
    }

    public void ClearState()
    {
        _currentState = null;
    }
}
