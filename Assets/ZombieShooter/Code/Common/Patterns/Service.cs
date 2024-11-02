using System;
using System.Linq.Expressions;

namespace ZombieShooter
{
    public static class Service<T> where T : class
    {
        private static T m_Instance;

        public static T Get()
        {
            return m_Instance;
        }

        public static T Register(T instance)
        {
            if (m_Instance != null)
            {
                throw new Exception($"Service {typeof(T).Name} already registered!");
            }
            m_Instance = instance;
            return m_Instance;
        }

        public static void Unregister()
        {
            if (m_Instance == null)
            {
                throw new Exception($"Service {typeof(T).Name} already unnregistered!");
            }

            m_Instance = null;
        }
    }
    
    public static class Service
    {
        public static T Get<T>() where T : class
        {
            return Service<T>.Get();
        }
        
        public static T Register<T>() where T : class, new()
        {
            return Service<T>.Register(new T());
        }
    }
}