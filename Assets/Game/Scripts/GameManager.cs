using FishNet.Connection;
using FishNet.Object;
using UnityEngine;


/// <summary>
/// Le chef d'orchestre de la partie. C'est un objet réseau placé dans la scène.
/// Pour l'instant, il fait apparaître un objet pour chaque joueur qui arrive.
/// </summary>
public class GameManager : NetworkBehaviour
{
    [SerializeField] private NetworkObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    
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

    public override void OnStopServer()
    {
        SceneManager.OnClientLoadedStartScenes -= OnClientLoadedStartScenes;
    }

    private void OnClientLoadedStartScenes(NetworkConnection conn, bool asServer)
    {
        if (asServer)
            SpawnPlayer(conn);
    }

    private void SpawnPlayer(NetworkConnection conn)
    {
        if (conn.FirstObject != null)
            return; // On ne spawn pas un joueur si il y en a un
        Transform spawn = spawnPoints[_nextSpawn % spawnPoints.Length];
        _nextSpawn++;
        NetworkObject player =Instantiate(playerPrefab, spawn.position, spawn.rotation);
        
        // Spawn = l'objet existe maintenant sur le réseau, et conn en est le propriétaire.
        Spawn(player, conn, gameObject.scene);
    }
}
