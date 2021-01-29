using System;
using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class PlayerControl : MonoBehaviourPun
    {
        private Camera cam;
        [SerializeField] private GameObject projectile;
        [SerializeField] private float moveForce;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Shoot(Vector2 target)
        {
            var rotation = Quaternion.LookRotation(Vector3.forward, target - (Vector2) transform.position);
            PhotonNetwork.Instantiate(projectile.name, transform.position, rotation);
            
        }

        private void Move(Vector2 shootTarget)
        {
            Vector2 moveDirection = (Vector2)transform.position - shootTarget;
            moveDirection.Normalize();
            //Debug.DrawRay(transform.position, moveDirection, Color.green);
            GetComponent<Rigidbody2D>().AddForce(moveDirection * moveForce, ForceMode2D.Impulse);
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                

                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 targetDirection = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Shoot(cam.ScreenToWorldPoint(targetDirection));
                    Move(cam.ScreenToWorldPoint(targetDirection));
                }
            }
        }
    }
}