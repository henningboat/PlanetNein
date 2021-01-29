using Runtime.Gameplay;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class StaticGravityObject : MonoBehaviour, IGravityObject
    {
        public float Radius => 10000;
        public float ForceMultiplier => 1;
        public Vector2 Position => transform.position;

        private void Start()
        {
            GravityObjectManager.Instance.AddObject(this);
        }

        private void OnDestroy()
        {
            GravityObjectManager.Instance.RemoveObject(this);
        }
    }
}