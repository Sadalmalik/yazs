using System.Threading.Tasks;
using IngameDebugConsole;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using UnityEngine;

namespace ZombieShooter
{
    public class NetworkManager
    {
        public async void Initialize()
        {
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

        // Host
        [ConsoleMethod( "CreateRelay", "Creates a relay" )]
        public async Task CreateRelay()
        {
            try
            {
                Debug.Log("Try create Relay");
                
                var allocation = await RelayService.Instance.CreateAllocationAsync(3);

                var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                
                Debug.Log($"Relay join code {joinCode}");

                RelayServerData data = new RelayServerData(allocation, "dtls"); // alternative "udp"
                
                Unity.Netcode.NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);

                Unity.Netcode.NetworkManager.Singleton.StartHost();
            }
            catch (RelayServiceException ex)
            {
                Debug.LogError(ex);
            }
        }
        
        // Client
        [ConsoleMethod( "JoinRelay", "joins relay" )]
        public async void JoinRelay(string joinCode)
        {
            try
            {
                var allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                
                RelayServerData data = new RelayServerData(allocation, "dtls"); // alternative "udp"
                
                Unity.Netcode.NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(data);

                Unity.Netcode.NetworkManager.Singleton.StartClient();
            }   
            catch (RelayServiceException ex)
            {
                Debug.LogError(ex);
            }
        }
        
    }
}