using Photon.Pun;
using PlanetNein.Runtime.CameraSystem;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerControl _playerPrefab;
        [SerializeField] private float _spawnDistance = 10;
        [SerializeField] private float _spawnVelocity = 7;


        private void Start()
        {
            var spawnPosition = Random.insideUnitCircle.normalized * _spawnDistance;
            var spawnedPlayer = PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity).GetComponent<GravityObject>();

            Vector2 startVelocity = Vector3.Cross(spawnedPlayer.transform.position, Vector3.back).normalized * _spawnVelocity;
            spawnedPlayer.AddForce(startVelocity);
        }
    }
}