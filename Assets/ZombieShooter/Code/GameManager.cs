using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZombieShooter
{
    public class GameManager : IService
    {
        private GameConfig _config;

        public GameManager(GameConfig config)
        {
            _config = config;
        }

        public void Initialize()
        {
            UnityEvents.OnUpdate += Tick;

            InitializeAsync();
            
            // UnityEvents.RunCoroutine(LoadMap(config.maps[0]));
        }

        public void Dispose()
        {
        }
        
        public async void InitializeAsync()
        {
            var scene = await new LoadSceneTask(_config.menuSceneName).Run();

            await Task.Delay(1000);
            
            LoadingScreen.Instance.Hide();
            MainMenuScreen.Instance.Show();
        }

        public async void LoadFirstMap()
        {
            await LoadingScreen.Instance.ShowAsync();
            
            // Under loading screen
            {
                MainMenuScreen.Instance.Hide();
                var map = _config.maps[0];
            
                await new LoadSceneTask(
                    map.SceneName,
                    true,
                    onProgress: progress =>
                    {
                        LoadingScreen.Instance.SetText($"{100 * progress:0.00}%");
                    }).Run();
            
                //LobbyScreen.Instance.Show();
            }
            
            await LoadingScreen.Instance.HideAsync();
            
            Debug.Log("Map loaded");
        }

        private LoadSceneTask _loadingTask;

        public IEnumerator LoadMap(MapConfig map)
        {
            _loadingTask = new LoadSceneTask(map.SceneName, true);
            yield return _loadingTask.AsyncOperation;

            Debug.Log("Scene loaded");
        }

        private void Tick()
        {
            if (_loadingTask != null)
            {
                Debug.Log($"Loading progress: {_loadingTask.Progress}");
            }
        }
    }
}