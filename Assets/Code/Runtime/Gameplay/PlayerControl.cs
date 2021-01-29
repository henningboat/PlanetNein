using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetNein
{
    public class PlayerControl : MonoBehaviour
    {
        private Camera cam;
        public GameObject Projectile;
        public int ProjectileForce;

        void Start()
        {
            cam = Camera.main;
        }

        void Shoot(Vector2 target)
        {
            GameObject newProjectile = Instantiate(Projectile, transform.position, Quaternion.identity);
            newProjectile.transform.LookAt(Vector3.forward, target - (Vector2)transform.position);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector3.forward * ProjectileForce);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                Shoot(cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)));
        }
    }
}