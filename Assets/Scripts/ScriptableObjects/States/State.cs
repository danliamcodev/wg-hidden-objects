using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "States/State")]
public class State : ScriptableObject
{
    private readonly List<IStateListener> _stateListeners = new List<IStateListener>();

    public void EnterState()
    {
        for (int i = _stateListeners.Count - 1; i >= 0; i--)
        {
            _stateListeners[i].OnStateEntered(this);
        }
    }

    public void ExitState()
    {
        for (int i = _stateListeners.Count - 1; i >= 0; i--)
        {
            _stateListeners[i].OnStateExited(this);
        }
    }

    public void RegisterListener(IStateListener p_listener)
    {
        if (!_stateListeners.Contains(p_listener))
            _stateListeners.Add(p_listener);
    }

    public void UnregisterListener(IStateListener p_listener)
    {
        if (_stateListeners.Contains(p_listener))
            _stateListeners.Remove(p_listener);
    }
}


public interface IStateListener
{
    void OnStateEntered(State p_state);

    void OnStateExited(State p_state);
}
