using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZombieShooter
{
    public class LoadSceneTask
    {
        private string _name;
        private bool _setActive;
        private LoadSceneMode _mode;
        private Action<Scene> _onComplete;
        private Action<float> _onProgress;

        public AsyncOperation AsyncOperation { get; private set; }
        public float Progress => AsyncOperation?.progress ?? 0;

        public LoadSceneTask(
            string name,
            bool setActive = false,
            LoadSceneMode mode = LoadSceneMode.Additive,
            Action<Scene> onComplete = null,
            Action<float> onProgress = null)
        {
            _name = name;
            _setActive = setActive;
            _mode = mode;
            _onComplete = onComplete;
            _onProgress = onProgress;
        }

        public async Task<Scene> Run()
        {
            var loadingOperation = SceneManager.LoadSceneAsync(_name, _mode);

            while (!loadingOperation.isDone)
            {
                await Task.Yield();
                _onProgress?.Invoke(loadingOperation.progress);
            }

            var scene = SceneManager.GetSceneByName(_name);
            if (_setActive)
                SceneManager.SetActiveScene(scene);

            _onComplete?.Invoke(scene);
            return scene;
        }
    }
}