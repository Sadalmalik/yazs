using UnityEditor;
using UnityEngine;

namespace ZombieShooter
{
    [CreateAssetMenu(
        fileName = "GameConfig",
        menuName = "[Zombie Shooter]/Game Config",
        order = 0)]
    public class GameConfig : ScriptableObject
    {
        public MapConfig[] maps;
    }
}