using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class Projectile : MonoBehaviourPun
    {
        private Rigidbody2D _rigidbody;
        [SerializeField] private float _startForce = 4;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Start()
        {
            if (photonView.IsMine)
            {
                _rigidbody.AddForce(transform.up * _startForce, ForceMode2D.Impulse);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damagable = other.GetComponent<DamageReceiver>();

            if (damagable == null)
            {
                return;
            }

            var canReceiveDamage = damagable.photonView.IsMine && !photonView.IsMine;

            if (canReceiveDamage)
            {
                damagable.Damage();
            }
        }
    }
}