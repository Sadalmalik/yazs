using System;
using System.Collections;

namespace ZombieShooter
{
    public class UnityEvents : SingletonBehaviour<UnityEvents>
    {
        public static event Action OnUpdate;
        public static event Action OnLateUpdate;
        public static event Action<bool> OnPause;

        public static void RunCoroutine(IEnumerator coroutine)
        {
            Instance.StartCoroutine(coroutine);
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }

        private void OnApplicationPause(bool pause)
        {
            OnPause?.Invoke(pause);
        }
    }
}