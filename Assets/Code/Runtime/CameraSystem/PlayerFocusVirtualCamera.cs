using Cinemachine;
using PlanetNein.Runtime.Gameplay;
using UnityEngine;

namespace PlanetNein.Runtime.CameraSystem
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class PlayerFocusVirtualCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        [SerializeField] private bool _focusOnWinner;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            PlayerControl target;
            if (_focusOnWinner)
            {
                target = GameLoop.Instance.Winner;
            }
            else
            {
                target = GameLoop.Instance.OwnPlayer;
            }

            if (target != null)
            {
                _virtualCamera.Follow = target.transform;
            }
        }
    }
}