using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Runtime.Multiplayer;
using THUtils;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class GameLoop : Singleton<GameLoop>
    {
        [Flags]
        public enum GameState : byte
        {
            GetReady = 1 << 0,
            Ingame = 1 << 1,

            //after all but one player are dead,
            //we wait for a second to check if the other player
            //will also die
            Cooldown = 1 << 2,
            PlayerWonRound = 1 << 3,
            Draw = 1 << 4,
            ReloadLevel = 1 << 5,
            PlayerWonGame = 1 << 6
        }

        [SerializeField] private float _cooldownTime = 2;

        public GameState CurrentGameState { get; private set; }
        [SerializeField] private float _getReadyDuration = 2;
        private float _lastStateSwitchTime;
        private PhotonView _photonView;

        private List<PlayerControl> _players = new List<PlayerControl>();

        public IReadOnlyList<PlayerControl> Players => _players;
        public PlayerControl OwnPlayer => _players.FirstOrDefault(playerControl => playerControl != null && playerControl.photonView.IsMine);
        
        private int PlayerAliveCount
        {
            get { return _players.Count(p => p != null); }
        }


        protected override void Awake()
        {
            base.Awake();
            _photonView = GetComponent<PhotonView>();
            CurrentGameState = GameState.GetReady;
        }
        

        private void Update()
        {
            if (_photonView.IsMine)
            {
                var newState = GetNewState();
                if (newState != CurrentGameState)
                {
                    _photonView.RPC("StateChange", RpcTarget.All, (byte) newState);
                }
            }
        }

        public void RegisterPlayer(PlayerControl playerControl)
        {
            _players.Add(playerControl);
        }

        [PunRPC]
        private void StateChange(byte state)
        {
            CurrentGameState = (GameState) state;
            _lastStateSwitchTime = Time.time;
            switch (CurrentGameState)
            {
                case GameState.GetReady:
                    break;
                case GameState.Ingame:
                    break;
                case GameState.Cooldown:
                    break;
                case GameState.PlayerWonRound:
                    StartCoroutine(RestartRoundDelayed());
                    break;
                case GameState.Draw:
                    StartCoroutine(RestartRoundDelayed());
                    break;
                case GameState.ReloadLevel:
                    break;
                case GameState.PlayerWonGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerator RestartRoundDelayed()
        {
            yield return new WaitForSeconds(3);
            PhotonNetwork.LoadLevel("MainScene");
        }

        private GameState GetNewState()
        {
            switch (CurrentGameState)
            {
                case GameState.GetReady:
                    if (Time.timeSinceLevelLoad > _getReadyDuration && _players.Count == PhotonNetwork.CurrentRoom.PlayerCount)
                    {
                        return GameState.Ingame;
                    }

                    break;
                case GameState.Ingame:

                    if (Lobby.IsDebugSession)
                    {
                        //in debug sessions, we end rounds by pressing F4
                        if (!Input.GetKeyDown(KeyCode.F4))
                        {
                            break;
                        }
                    }

                    if (PlayerAliveCount <= 1)
                    {
                        return GameState.Cooldown;
                    }

                    break;
                case GameState.Cooldown:
                    if (Time.time - _lastStateSwitchTime > _cooldownTime)
                    {
                        if (PlayerAliveCount == 0)
                        {
                            return GameState.Draw;
                        }

                        return GameState.PlayerWonRound;
                    }

                    break;
                case GameState.PlayerWonRound:
                    break;
                case GameState.Draw:
                    break;
                case GameState.ReloadLevel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return CurrentGameState;
        }
    }
}