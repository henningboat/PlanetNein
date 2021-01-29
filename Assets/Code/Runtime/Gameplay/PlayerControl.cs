using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class PlayerControl : MonoBehaviourPun
    {
        private Camera cam;
        public GameObject Projectile;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Shoot(Vector2 target)
        {
            var rotation = Quaternion.LookRotation(Vector3.forward, target - (Vector2) transform.position);
            PhotonNetwork.Instantiate(Projectile.name, transform.position, rotation);
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot(cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)));
                }
            }
        }
    }
}