using System;
using Unity.Netcode;

namespace ZombieShooter
{
    [Serializable]
    public class GameState : NetworkBehaviour
    {
        public string Title;
        public string GameMode;
        public string Map;
    }
}