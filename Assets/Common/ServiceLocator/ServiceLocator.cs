using UnityEngine;

namespace Services
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ServiceLocator
    {
        private Dictionary<string, IService> m_Services = new Dictionary<string, IService>();
        public static ServiceLocator Current;

        public ServiceLocator()
        {
            if (Current == null)
                Current = this;
            else
                return;
        }

        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (m_Services.ContainsKey(key))
            {
                Debug.LogError(key + " was register");
                return;
            }
            m_Services.Add(key, service);
        }
        public void Unregister<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (!m_Services.ContainsKey(key))
            {
                Debug.LogError(key + " was not register. Can't remove");
                return;
            }
            m_Services.Remove(key);
        }
        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!m_Services.ContainsKey(key))
            {
                Debug.Log(key + " was not register. Can't get");
                throw new InvalidOperationException();
            }

            return (T)m_Services[key];
        }

        public void UnregisterAll()
        {
            m_Services.Clear();
        }
    }
}
