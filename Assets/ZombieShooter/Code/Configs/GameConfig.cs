using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace ZombieShooter
{
    [CreateAssetMenu(
        fileName = "GameConfig",
        menuName = "[Zombie Shooter]/Game Config",
        order = 0)]
    public class GameConfig : ScriptableObject
    {
        public string menuSceneName;
        public MapConfig[] maps;
        public Unit unitPrefab;
    }
}