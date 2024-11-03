using System;
using System.Linq.Expressions;

namespace ZombieShooter
{
    public interface IService
    {
        void Initialize();
        void Dispose();
    }
    
    public static class Service
    {
        public static T Get<T>() where T : class, IService
        {
            return Service<T>.Get();
        }
        
        public static T Register<T>(T service) where T : class, IService
        {
            return Service<T>.Register(service);
        }
        
        public static T Register<T>() where T : class, IService, new()
        {
            return Service<T>.Register(new T());
        }
    }
    
    public static class Service<T> where T : class, IService
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
            m_Instance.Initialize();
            return m_Instance;
        }

        public static void Unregister()
        {
            if (m_Instance == null)
            {
                throw new Exception($"Service {typeof(T).Name} already unnregistered!");
            }
            m_Instance.Dispose();
            m_Instance = null;
        }
    }
}