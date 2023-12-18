using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<Type, Event, UnityEventObject> : MonoBehaviour, IGameEventListener<Type>
    where Event : BaseGameEvent<Type> where UnityEventObject : UnityEvent<Type>
{
    /*
    [Tooltip("Event to register with.")]
    [SerializeField] Event _event;

    [Tooltip("Response to invoke when Event is raised.")]
    [SerializeField] UnityEventObject _response;
    */

    [System.Serializable]
    public class EventResponse
    {
        [Tooltip("Event to register with.")]
        [SerializeField] Event _event;

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] UnityEventObject _response;

        public Event gameEvent { get { return _event; } }
        public UnityEventObject response { get { return _response; } }
    }

    [SerializeField] List<EventResponse> _events;
    Dictionary<BaseGameEvent<Type>, UnityEventObject> _responses = new Dictionary<BaseGameEvent<Type>, UnityEventObject>();

    private void OnEnable()
    {
        foreach (EventResponse _eventResponse in _events)
        {
            _responses.Add(_eventResponse.gameEvent, _eventResponse.response);
            _eventResponse.gameEvent.RegisterListener(this);
        }

        //_event.RegisterListener(this);
    }

    private void OnDisable()
    {
        foreach (EventResponse _eventResponse in _events)
        {
            _eventResponse.gameEvent.UnregisterListener(this);
        }
        _responses.Clear();
        //_event.UnregisterListener(this);
    }

    public void OnEventRaised(Type p_parameter, BaseGameEvent<Type> p_baseGameEvent)
    {
        //_response.Invoke(p_parameter);
        _responses[p_baseGameEvent].Invoke(p_parameter);
    }
}


