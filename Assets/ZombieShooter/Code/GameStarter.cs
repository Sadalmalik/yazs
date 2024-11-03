using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

namespace ZombieShooter
{
    public class GameStarter : MonoBehaviour
    {
        public GameConfig config;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            
            Service.Register<NetworkManager>();
            Service.Register(new GameManager(config));
        }

        private void OnApplicationQuit()
        {
            Service<NetworkManager>.Unregister();
            Service<GameManager>.Unregister();
        }
    }
}