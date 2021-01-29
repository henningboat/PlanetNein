using Photon.Pun;
using Runtime.Multiplayer;
using UnityEngine;

namespace PlanetNein.Runtime.Multiplayer
{
    public class EditorSessionStarter : MonoBehaviour
    {
        private void Start()
        {
            if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
            {
                Lobby.EditorConnectSinglePlayer();
            }
        }
    }
}