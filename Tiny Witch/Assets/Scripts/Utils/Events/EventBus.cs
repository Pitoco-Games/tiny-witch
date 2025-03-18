using System;
using System.Collections.Generic;

namespace Utils.Events
{
    public class EventBus
    {
        private static EventBus instance;
        public static EventBus Instance 
        {
            get
            { 
                if(instance == null)
                {
                    instance = new EventBus();
                }

                return instance;
            } 
        }

        private Dictionary<Type, List<Action<BaseEvent>>> eventTypeToListeners = new();

        public void AddListener<T>(Action<BaseEvent> listener)
        {
            if(eventTypeToListeners.TryGetValue(typeof(T), out List<Action<BaseEvent>> listeners))
            {
                listeners.Add(listener);
            }
            else
            {
                eventTypeToListeners[typeof(T)] = new List<Action<BaseEvent>> { listener };
            }
        }

        public void RemoveListener<T>(Action<BaseEvent> listener)
        {
            if (eventTypeToListeners.TryGetValue(typeof(T), out List<Action<BaseEvent>> listeners))
            {
                listeners.Remove(listener);

                if(listeners.Count == 0)
                {
                    eventTypeToListeners.Remove(typeof(T));
                }
            }
        }

        public void InvokeEvent(object eventInstance)
        {
            if (!eventTypeToListeners.TryGetValue(eventInstance.GetType(), out List<Action<BaseEvent>> listeners))
            {
                return;
            }

            foreach (Action<object> listener in listeners)
            { 
                listener.Invoke(eventInstance);
            }
        }
    }
}
