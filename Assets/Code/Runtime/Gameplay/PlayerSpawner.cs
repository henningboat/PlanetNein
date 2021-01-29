using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerControl _playerPrefab;
        [SerializeField] private float _spawnDistance=10;
        [SerializeField] private float _spawnVelocity=7;
        
        
        private void Start()
        {
            var spawnDistance = Random.insideUnitCircle.normalized * _spawnDistance;
            var spawnedPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, spawnDistance, Quaternion.identity).GetComponent<GravityObject>();

            Vector2 startVelocity = Vector3.Cross(transform.position, Vector3.back).normalized * _spawnVelocity;
            spawnedPlayer.AddForce(startVelocity);
        }
    }
}