using PlanetNein.Runtime.Gameplay;
using THUtils;
using UnityEngine;

namespace PlanetNein.Runtime.UI
{
    public class RoundEndScreen : Singleton<RoundEndScreen>
    {
        [SerializeField] private GameObject _playerLostScreen;
        [SerializeField] private GameObject _playerWonScreen;

        public void ShowScreen()
        {
            var localPlayerWon = GameLoop.Instance.Winner.photonView.IsMine;
            _playerWonScreen.SetActive(localPlayerWon);
            _playerLostScreen.SetActive(!localPlayerWon);
        }
    }
}