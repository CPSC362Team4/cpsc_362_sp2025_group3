using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : NetworkBehaviour
{
    private int loadedPlayers;
    public int totalPlayers;
    public static SceneManager Singleton { get; set; }
    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        DontDestroyOnLoad(gameObject);
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        NetworkManager.SceneManager.OnLoadComplete += OnSceneLoaded;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnect;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;

    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    public void loadClientScenesServerRpc(string sceneName)
    {

        if (sceneName == "Fake") { return; }

        loadedPlayers = 0;
        ResetCountStuffRpc();
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    [Rpc(SendTo.ClientsAndHost, RequireOwnership = false)]
    private void ResetCountStuffRpc()
    {
        loadedPlayers = 0;

    }
    private void OnClientConnect(ulong id)
    {
        totalPlayers += 1;
        Debug.Log("Someone connected ");
    }
    private void OnClientDisconnect(ulong id)
    {
        totalPlayers -= 1;
    }

   
    
    private void OnSceneLoaded(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        loadedPlayers += 1;
        Debug.Log("Loaded PLayers " + loadedPlayers);
    }

    public bool everyoneLoaded()
    {
        return loadedPlayers >= TurnManager.Singleton.totalPlayers;
    }
}
