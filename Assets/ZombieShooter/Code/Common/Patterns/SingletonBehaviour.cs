using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ZombieShooter
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        static SingletonBehaviour<T> m_Instance;

        public abstract bool AllowInstantiate
        {
            get;
        }
        
        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    m_Instance = go.AddComponent<T>();
                    if (!m_Instance.AllowInstantiate)
                    {
                        m_Instance = null;
                        Destroy(go);
                        throw new Exception($"Singleton {typeof(T).Name} can't be instantiated via code! Please create it from prefab!");
                    }
                    DontDestroyOnLoad(go);
                }
                return (T) m_Instance;
            }
        }

        private void Awake()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                Initialize();
            }
            else
            {
                enabled = false;
                Destroy(this);
            }
        }

        protected virtual void Initialize()
        {
            
        }
    }
}