using Runtime.Gameplay;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GravityObject : MonoBehaviour
    {
        [SerializeField] private float _gravityStrength = 2;
        [SerializeField] private float _radius = 10;

        private Rigidbody2D _rigidbody;
        [SerializeField] private Vector2 _startForce;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rigidbody.AddForce(_startForce, ForceMode2D.Impulse);
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
            foreach (var other in GravityObjectManager.Instance.GravityObjects)
            {
                if (other == this)
                {
                    continue;
                }

                if (Vector2.Distance(transform.position, other.transform.position) < _radius + other._radius)
                {
                    Vector2 addForce = (other.transform.position - transform.position).normalized * (_gravityStrength * Time.deltaTime);
                    _rigidbody.AddForce(addForce, ForceMode2D.Force);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
            Gizmos.DrawRay(transform.position, _startForce);
        }
    }
}