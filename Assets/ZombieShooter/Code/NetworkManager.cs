using System.Collections.Generic;
using System.Threading.Tasks;
using IngameDebugConsole;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using UnityEngine;

namespace ZombieShooter
{
    public class NetworkManager : IService
    {
        public const string DataRelay = "Relay";
        public async void Initialize()
        {
            UnityEvents.OnUpdate += Tick;
            
            try
            {
                await UnityServices.InitializeAsync();

                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                Debug.Log($"Signed in as {AuthenticationService.Instance.PlayerId}");

                // await CreateRelay();
            }
            catch (AuthenticationException ex)
            {
                Debug.LogError(ex);
            }
        }

        public void Dispose()
        {
            AuthenticationService.Instance.SignOut();
        }

        private void Tick()
        {
            LobbyHeartbeat();
        }

#region Our API

        public async void CreateGame(bool isPrivate = false)
        {
            // Сначала создаем релэй
            var relayCode = await CreateRelay();
            
            // Затем создаем лобби с кодом релея
            var lobby = await CreateLobby(relayCode, isPrivate);
        }

        public async void JoinGame(string lobbyCode)
        {
            // Сначала подклюачемся к лобби
            var lobby = await JoinLobbyByCode(lobbyCode);
            
            // Затем подключаемся к релею
            await JoinRelay(lobby.Data[DataRelay].Value);
        }

        public List<string> SearchGame()
        {
            return null;
        }

#endregion

        
#region LobbyLogic

        public Lobby CurrentLobby;
        public string CurrentLobbyJoinCode;

        public const float heartbeatDuration = 15;
        private float _nextHeartbeat;
        
        private async Task<Lobby> CreateLobby(string relayCode, bool isPrivate)
        {
            try
            {
                var options = new CreateLobbyOptions()
                {
                    IsPrivate = isPrivate,
                    Data =
                    {
                        { DataRelay, new DataObject(DataObject.VisibilityOptions.Member, relayCode) }
                    }
                };
                CurrentLobby = await LobbyService.Instance.CreateLobbyAsync(
                    "No Name Supported", 4, options);

                CurrentLobbyJoinCode = CurrentLobby.LobbyCode;
                
                return CurrentLobby;
            }
            catch (LobbyServiceException ex)
            {
                Debug.LogError(ex);
            }

            return null;
        }

        private async Task LobbyHeartbeat()
        {
            if (CurrentLobby == null)
                return;
            
            if (Time.unscaledTime < _nextHeartbeat)
                return;
            
            _nextHeartbeat = Time.unscaledTime + heartbeatDuration;

            // Why await here?
            await LobbyService.Instance.SendHeartbeatPingAsync(CurrentLobby.Id);
        }

        private async Task<Lobby> JoinLobbyByCode(string lobbyCode)
        {
            try
            {
                CurrentLobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode);

                return CurrentLobby;
            }
            catch (LobbyServiceException ex)
            {
                Debug.LogError(ex);
            }
            
            return null;
        }

        private async Task<Lobby> JoinLobbyByID(string lobbyId)
        {
            try
            {
                CurrentLobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId);

                return CurrentLobby;
            }
            catch (LobbyServiceException ex)
            {
                Debug.LogError(ex);
            }
            
            return null;
        }

#endregion


#region Relay logic

        public string RelayJoinCode { get; private set; }


        // Host
        private async Task<string> CreateRelay()
        {
            try
            {
                var allocation = await RelayService.Instance.CreateAllocationAsync(3);

                RelayJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

                RelayServerData data = new RelayServerData(allocation, "dtls"); // alternative "udp"

                Unity.Netcode.NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);

                Unity.Netcode.NetworkManager.Singleton.StartHost();

                return RelayJoinCode;
            }
            catch (RelayServiceException ex)
            {
                Debug.LogError(ex);
            }

            return null;
        }

        // Client
        public async Task JoinRelay(string joinCode)
        {
            try
            {
                var allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

                RelayServerData data = new RelayServerData(allocation, "dtls"); // alternative "udp"

                Unity.Netcode.NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);

                Unity.Netcode.NetworkManager.Singleton.StartClient();
                
                RelayJoinCode = joinCode;
            }
            catch (RelayServiceException ex)
            {
                Debug.LogError(ex);
            }
        }

#endregion
    }
}