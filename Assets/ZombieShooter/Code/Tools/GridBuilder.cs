using System;
using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    [ExecuteInEditMode]
    public class GridBuilder : MonoBehaviour
    {
#if UNITY_EDITOR
        public Transform prefab;
        public Vector3Int dimension;
        
        [Space]
        public Vector3 xStep = Vector3.right;
        public Vector3 yStep = Vector3.up;
        public Vector3 zStep = Vector3.forward;

        [Space]
        public bool build;
        
        void Update()
        {
            if (build)
            {
                build = false;
                
                transform.DestroyChildren();
            
                for (int y = 0; y < dimension.y; y++)
                for (int z = 0; z < dimension.z; z++)
                for (int x = 0; x < dimension.x; x++)
                {
                    var item = Instantiate(prefab, transform);
                    item.localPosition = x * xStep + y * yStep + z * zStep;
                }
            }
        }
#endif
    }
}