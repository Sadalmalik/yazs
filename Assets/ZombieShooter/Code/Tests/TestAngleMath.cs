using UnityEngine;

namespace ZombieShooter
{
    [ExecuteInEditMode]
    public class TestAngleMath : MonoBehaviour
    {
        public float angleA = 0;
        public float angleB = 0;
        public float angleC = 0;
        
        public void Update()
        {
            angleA = AngleMath.Normalize(angleA);
            angleB = AngleMath.Normalize(angleB);
            angleC = AngleMath.Sub(angleA, angleB);
        }
    }
}