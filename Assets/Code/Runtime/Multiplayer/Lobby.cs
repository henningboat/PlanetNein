using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Runtime.Multiplayer
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        #region Unity methods

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Static Stuff

        public static Lobby Instance { get; private set; }

        private static void CheckRoomJoined()
        {
            if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.LoadLevel("MainScene");
            }
        }

        #endregion

        #region Private Fields

        #endregion

        public void Start()
        {
            Connect();
        }

        #region Public methods

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                Debug.LogError("Photon is already connected when Connect() is called in Lobby, this should never happen");
            }

            PhotonNetwork.AutomaticallySyncScene = true;

            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinOrCreateRoom("Public Match", new RoomOptions {MaxPlayers = 2}, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            CheckRoomJoined();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            Application.Quit();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            CheckRoomJoined();
        }

        #endregion
    }
}