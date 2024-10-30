using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            var toRemove = new List<GameObject>();
            
            foreach (Transform child in transform)
            {
                toRemove.Add(child.gameObject);
            }

            foreach (var child in toRemove)
            {
                if (Application.isPlaying)
                {
                    Object.Destroy(child);
                }
                else
                {
                    Object.DestroyImmediate(child);
                }
            }
        }
    }
}