using Photon.Pun;
using Photon.Realtime;
using Runtime.Multiplayer;
using UnityEngine;

namespace PlanetNein.Runtime.Multiplayer
{
    public class EditorSessionStarter : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom)
            {
                Lobby.EditorConnectSinglePlayer();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            Lobby.OnError();
        }
    }
}