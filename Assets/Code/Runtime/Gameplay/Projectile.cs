using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class Projectile : MonoBehaviourPun
    {
        [SerializeField] private float _lifetime;
        private Rigidbody2D _rigidbody;

        [SerializeField] private float _startForce = 4;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public IEnumerator Start()
        {
            if (photonView.IsMine)
            {
                _rigidbody.AddForce(transform.up * _startForce, ForceMode2D.Impulse);
                
                yield return new WaitForSeconds(_startForce);
                
                PhotonNetwork.Destroy(photonView);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damagable = other.GetComponent<DamageReceiver>();

            if (damagable == null)
            {
                return;
            }

            var canReceiveDamage = damagable.photonView.IsMine;
            if (damagable.IsPlayer)
            {
                canReceiveDamage &= photonView.IsMine == false;
            }

            if (canReceiveDamage)
            {
                damagable.Damage();
            }
        }
    }
}