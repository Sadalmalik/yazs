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
            UnityEvents.RunCoroutine(LoadMap(config.maps[0]));
        }

        public IEnumerator LoadMap(MapConfig map)
        {
            yield return SceneManager.LoadSceneAsync(map.SceneName, LoadSceneMode.Additive);
            
            Debug.Log("Scene loaded");
        }
    }
}