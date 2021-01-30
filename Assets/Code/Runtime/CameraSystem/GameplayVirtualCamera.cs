using Cinemachine;
using Runtime.Gameplay;
using THUtils;
using UnityEngine;

namespace PlanetNein.Runtime.CameraSystem
{
    public class GameplayVirtualCamera : Singleton<GameplayVirtualCamera>
    {
        private CinemachineVirtualCamera _virtualCamera;
        private float _zoomDistance = 20;

        protected override void Awake()
        {
            base.Awake();
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            float targetZoom = 0;
            foreach (var gravityObject in GravityObjectManager.Instance.GravityObjects)
            {
                if(gravityObject.AffectCamera)
                    continue;
                
                var cameraPosition = gravityObject.Position;
                cameraPosition.x /= _virtualCamera.m_Lens.Aspect;
                targetZoom = Mathf.Max(targetZoom, cameraPosition.magnitude);
            }

            if (_zoomDistance < targetZoom)
            {
                _zoomDistance = Mathf.Lerp(_zoomDistance, targetZoom + 4, Time.deltaTime * 3);
            }

            _virtualCamera.m_Lens.OrthographicSize = _zoomDistance;
        }
    }
}