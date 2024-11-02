using UnityEngine;

namespace ZombieShooter
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        static SingletonBehaviour<T> m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    m_Instance = go.AddComponent<T>();
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
            }
            else
            {
                enabled = false;
                Destroy(this);
            }
        }
    }
}