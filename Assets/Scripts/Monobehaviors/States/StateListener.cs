using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateListener : MonoBehaviour, IStateListener
{
    [Header("Parameters")]
    [SerializeField] List<StateResponses> _states;
    Dictionary<State, UnityEvent> _onStateEnteredResponses = new Dictionary<State, UnityEvent>();
    Dictionary<State, UnityEvent> _onStateExitedResponses = new Dictionary<State, UnityEvent>();

    private void OnEnable()
    {
        foreach (StateResponses _stateResponses in _states)
        {
            _onStateEnteredResponses.Add(_stateResponses.state, _stateResponses.onStateEnteredResponse);
            _onStateExitedResponses.Add(_stateResponses.state, _stateResponses.onStateExitedResponse);
            _stateResponses.state.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        foreach (StateResponses _stateResponses in _states)
        {
            _stateResponses.state.UnregisterListener(this);
        }
        _onStateEnteredResponses.Clear();
        _onStateExitedResponses.Clear();
    }

    public void OnStateEntered(State p_state)
    {
        _onStateEnteredResponses[p_state].Invoke();
    }


    public void OnStateExited(State p_state)
    {
        _onStateExitedResponses[p_state].Invoke();
    }

    [System.Serializable]
    public class StateResponses
    {
        [Tooltip("State to register with.")]
        [SerializeField] State _state;

        [Tooltip("Response to invoke when State is entered.")]
        [SerializeField] UnityEvent _onStateEnteredResponse;

        [Tooltip("Response to invoke when State is exited.")]
        [SerializeField] UnityEvent _onStateExitedResponse;

        public State state => _state;
        public UnityEvent onStateEnteredResponse => _onStateEnteredResponse;
        public UnityEvent onStateExitedResponse => _onStateExitedResponse;
    }
}
