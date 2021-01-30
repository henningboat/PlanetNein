using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Runtime.Multiplayer
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        public static bool IsDebugSession { get; private set; }

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
                PhotonNetwork.CurrentRoom.IsVisible = false;
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

        /// <summary>
        /// Loads LobbyScene, 
        /// Gets called by EditorSessionStarter in Editor if the game is started from MainScene
        /// </summary>
        public static void EditorConnectSinglePlayer()
        {
            IsDebugSession = true;
            SceneManager.LoadScene("Lobby");
        }
        
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
            byte maxPlayers = (byte) (IsDebugSession ? 1 : 2);
            
            var roomName = IsDebugSession ? UnityEngine.Random.value.ToString() : "PublicMatch";
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions {MaxPlayers = maxPlayers}, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            CheckRoomJoined();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            Debug.LogError(message);
            OnError();
        }

        public static void OnError()
        {
#if UNITY_EDITOR
            if (Application.isEditor)
                EditorApplication.ExitPlaymode();
            else
#endif
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