using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using UnityEngine;


/// <summary>
/// Le chef d'orchestre de la partie. C'est un objet réseau placé dans la scène.
/// Pour l'instant, il fait apparaître un objet pour chaque joueur qui arrive.
/// </summary>
public class GameManager : NetworkBehaviour
{
    [SerializeField] private NetworkManager PlayerPrefab;
    [SerializeField] private Transform[] SpawnPoints;
    
    private int _nextSpawn; // Serveur uniquement; le prochain point de spawn a utiliser.

    public override void OnStartServer()
    {
        //On dit aux clients de faire spawn un player au démarrage
        SceneManager.OnClientLoadedStartScenes += OnClientLoadedStartScenes;
        //Les clients qui ont chargé avant que le script démarre
        foreach (NetworkConnection conn in ServerManager.Clients.Values)
            if (conn.LoadedStartScenes(true))
                SpawnPlayer(conn); 
    }

    private void OnClientLoadedStartScenes(NetworkConnection conn, bool asServer)
    {
        
    }
    private void SpawnPlayer(NetworkConnection conn)
    {
        
    }
    

}
