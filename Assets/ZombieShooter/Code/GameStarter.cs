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
            
            Service.Register<NetworkManager>().Initialize();
            Service.Register<GameManager>().Initialize(config);
        }
    }
}