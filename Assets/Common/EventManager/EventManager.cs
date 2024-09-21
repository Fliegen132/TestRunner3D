using Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventManager : IService
    {
        private Dictionary<Type,Dictionary<object, object>> m_Events;

        public EventManager()
        {
            m_Events = new Dictionary<Type, Dictionary<object, object>>();
        }

        private bool CheckSub<T>(object listener)
        {
            var type = typeof(T);
            if (!m_Events.ContainsKey(type))
            {
                m_Events.Add(type, new Dictionary<object, object>());
            }

            if (m_Events[type].ContainsKey(listener))
            {
                Debug.LogError($"you try to subscribe on {type} twice with object: {listener} type: {listener.GetType()}");
                return false;
            }

            return true;
        }

        public void Subscribe<T>(object listener, Action action) where T : class
        {
            if (CheckSub<T>(listener))
            {
                var type = typeof(T);

                m_Events[type].Add(listener, action);
            }
        }

        public void Subscribe<T, U>(object listener, Action<U> action) where T : class
        {
            if (CheckSub<T>(listener))
            {
                var type = typeof(T);

                m_Events[type].Add(listener, action);
            }
        }

        public void Unsubscribe<T>(object listener) where T : class
        { 
            var type = typeof(T);
            if (m_Events.ContainsKey(type))
            {
                m_Events[type].Remove(listener);
                m_Events.Remove(type);
            }
        }

        public void TriggerEvenet<T>()
        {
            var type = typeof(T);

            if (m_Events.ContainsKey(type))
            { 
                if(m_Events.TryGetValue(type, out var events))
                {
                    foreach (var myEvent in events)
                    {
                        Action action = myEvent.Value as Action;
                        if (action == null)
                        {
                            Debug.LogError($"u trigger {type}, u not add action");
                        }

                        action.Invoke();
                    }
                }
            }
        }

        public void TriggerEvenet<T, U>(U arg) 
        {
            var type = typeof(T);

            if (m_Events.ContainsKey(type))
            {
                if (m_Events.TryGetValue(type, out var events))
                {
                    foreach (var myEvent in events)
                    {
                        Action<U> action = myEvent.Value as Action<U>;
                        if (action == null)
                        {
                            Debug.LogError($"u trigger {type}, u not add action");
                        }

                        action.Invoke(arg);
                    }
                }
            }
        }
    }
}
