using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerControl _playerPrefab;

        private void Start()
        {
            PhotonNetwork.Instantiate(_playerPrefab.name, Random.insideUnitCircle.normalized * 5, Quaternion.identity);
        }
    }
}