using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class Projectile : MonoBehaviourPun
    {
        private bool _appliedDamage;
        [SerializeField] private float _lifetime;
        private Rigidbody2D _rigidbody;

        [SerializeField] private float _startForce = 4;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public IEnumerator Start()
        {
            _appliedDamage = false;
            if (photonView.IsMine)
            {
                _rigidbody.AddForce(transform.up * _startForce, ForceMode2D.Impulse);

                yield return new WaitForSeconds(_lifetime);

                PhotonNetwork.Destroy(photonView);
            }
        }

        public void AddForce(Vector2 force)
        {
            _rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_appliedDamage)
            {
                return;
            }

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
                _appliedDamage = true;
                photonView.RPC("DestroyProjectile", RpcTarget.AllViaServer);
            }
        }

        [PunRPC]
        private void DestroyProjectile()
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}