using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

namespace ZombieShooter
{
    public class GameStarter : MonoBehaviour
    {
        public GameConfig config;

        private GameManager _GameManager;

        void Start()
        {
            _GameManager = new GameManager();
            _GameManager.Initialize(config);
        }
    }
}