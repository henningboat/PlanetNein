using Photon.Pun;
using Runtime.Gameplay;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityObject : MonoBehaviourPun, IGravityObject
    {
        [SerializeField] private float _gravityStrength = 2;
        [SerializeField] private float _radius = 10;

        private Rigidbody2D _rigidbody;
        [SerializeField] private Vector2 _startForce;
        [SerializeField] private float forceMultiplier=1;
        [SerializeField] private bool _affectCamera=true;

        public float Radius => _radius;
        public float ForceMultiplier => forceMultiplier;
        public Vector2 Position => transform.position;
        public bool AffectCamera => _affectCamera;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidbody.isKinematic = photonView.IsMine == false;
            
            if (photonView.IsMine)
            {
                _rigidbody.AddForce(transform.localToWorldMatrix.MultiplyVector(_startForce), ForceMode2D.Impulse);
            }

            GravityObjectManager.Instance.AddObject(this);
        }

        private void OnDestroy()
        {
            if (GravityObjectManager.Instance != null)
            {
                GravityObjectManager.Instance.RemoveObject(this);
            }
        }

        private void FixedUpdate()
        {
            if (photonView.IsMine)
            {
                foreach (var other in GravityObjectManager.Instance.GravityObjects)
                {
                    if (ReferenceEquals(other, this))
                    {
                        continue;
                    }

                    if (Vector2.Distance(transform.position, other.Position) < Radius + other.Radius)
                    {
                        Vector2 addForce = (other.Position - Position).normalized * (_gravityStrength * Time.deltaTime * other.ForceMultiplier);
                        _rigidbody.AddForce(addForce, ForceMode2D.Force);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, Radius);
            Gizmos.DrawRay(Vector3.zero, _startForce);
        }

        public void AddForce(Vector2 velocity)
        {
            _rigidbody.AddForce(velocity, ForceMode2D.Impulse);
        }
    }
}