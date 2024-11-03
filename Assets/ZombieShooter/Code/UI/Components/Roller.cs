using System;
using UnityEngine;

namespace ZombieShooter
{
    public class Roller : MonoBehaviour
    {
        public RectTransform target;

        public float speed;

        private void Update()
        {
            target.localRotation = Quaternion.Euler(0, 0, Time.time * speed);
        }
    }
}