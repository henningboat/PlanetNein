using Cinemachine;
using PlanetNein.Runtime.Gameplay;
using UnityEngine;

namespace PlanetNein.Runtime.CameraSystem
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class PlayerFocusVirtualCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            var currentPlayer = GameLoop.Instance.OwnPlayer;
            if (currentPlayer != null)
            {
                _virtualCamera.Follow = currentPlayer.transform;
            }
        }
    }
}