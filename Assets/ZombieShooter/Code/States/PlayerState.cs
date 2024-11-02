using System;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZombieShooter
{
    [Serializable]
    public class PlayerState : NetworkBehaviour
    {
        public static readonly FixedString64Bytes[] Names = {
            "Tolyan",
            "Vlad",
            "Egorardo",
            "Elon Musk",
        };
        
        private NetworkVariable<FixedString64Bytes> m_Name = new NetworkVariable<FixedString64Bytes>();
        private NetworkVariable<Color> m_ColorMain = new NetworkVariable<Color>();
        private NetworkVariable<Color> m_ColorAdditional = new NetworkVariable<Color>();
        
        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                m_Name.Value = Names[Random.Range(0, Names.Length)];
                // NetworkManager.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
                OnNameChanged("", m_Name.Value);
            }
            else
            {
                m_Name.OnValueChanged += OnNameChanged;
            }
        }

        private void OnNameChanged(FixedString64Bytes prev, FixedString64Bytes curr)
        {
            gameObject.name = $"PlayerState: {curr}";
        }
    }
}