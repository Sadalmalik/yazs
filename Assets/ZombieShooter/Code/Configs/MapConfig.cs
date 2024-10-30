using UnityEngine;

namespace ZombieShooter
{
    [CreateAssetMenu(
        fileName = "Map",
        menuName = "[Zombie Shooter]/Map",
        order = 0)]
    public class MapConfig : ScriptableObject
    {
        [Header("Description")]
        public string Name;
        public string Description;
        public string SceneName;

        [Header("Settings")]
        public int playersLimit = 4;
        public float RoundDuration = 15 * 60;
        public int Reward = 1500;
    }
}