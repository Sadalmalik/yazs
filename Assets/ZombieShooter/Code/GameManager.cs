using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZombieShooter
{
    public class GameManager
    {
        public void Initialize(GameConfig config)
        {
            UnityEvents.OnUpdate += Tick;
            UnityEvents.RunCoroutine(LoadMap(config.maps[0]));
        }

        private AsyncOperation _loading;

        public IEnumerator LoadMap(MapConfig map)
        {
            _loading = SceneManager.LoadSceneAsync(map.SceneName, LoadSceneMode.Additive);
            yield return _loading;
            _loading = null;
            
            Debug.Log("Scene loaded");
        }

        private void Tick()
        {
            if (_loading != null)
            {
                Debug.Log($"Loading progress: {_loading.progress}");
            }
        }
    }
}