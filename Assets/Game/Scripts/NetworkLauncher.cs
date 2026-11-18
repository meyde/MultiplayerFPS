using UnityEngine;
using FishNet.Managing;
/// <summary>
/// Toute la logique de connexion : héberger, rejoindre.
/// Placé sur le même objet que le NetworkManager.
/// Pour l'instant, deux boutons de test l'appellent directement.
/// </summary>
[RequireComponent(typeof(NetworkManager))]
public class NetworkLauncher : MonoBehaviour
{
    private NetworkManager _networkManager;
    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
    }

    #region  Actions changées par l'UI

    /// <summary> Héberger : on devient serveur ET joueur (host).</summary>
    public void Host()
    {
        _networkManager.ServerManager.StartConnection();
        _networkManager.ClientManager.StartConnection("localhost");
    }

    public void Join(string ip)
    {
        _networkManager.ClientManager.StartConnection(ip);
    }

    #endregion

}
