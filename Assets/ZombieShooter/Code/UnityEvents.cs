using System.Collections;

namespace ZombieShooter
{
    public class UnityEvents : SingletonBehaviour<UnityEvents>
    {
        public static void RunCoroutine(IEnumerator coroutine)
        {
            Instance.StartCoroutine(coroutine);
        }
    }
}