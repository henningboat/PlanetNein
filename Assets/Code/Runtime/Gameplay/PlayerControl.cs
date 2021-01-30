using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class PlayerControl : MonoBehaviourPun
    {
        private float _lastShotTime;
        [SerializeField] private float _reloadTime = 3;
        private Rigidbody2D _rigidbody;
        private Camera cam;

        [SerializeField] private GameObject projectile;
        [SerializeField] private float shotPushForce;
        [SerializeField] private float steeringPushForce;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            cam = Camera.main;
            GameLoop.Instance.RegisterPlayer(this);
        }

        private void Shoot(Vector2 target)
        {
            var rotation = Quaternion.LookRotation(Vector3.forward, target - (Vector2) transform.position);
            var projectileInstance = PhotonNetwork.Instantiate(projectile.name, transform.position, rotation).GetComponent<Projectile>();
            projectileInstance.AddForce(_rigidbody.velocity);

            ApplyShotPush(target);
        }

        private void ApplyShotPush(Vector2 shootTarget)
        {
            var moveDirection = (Vector2) transform.position - shootTarget;
            moveDirection.Normalize();
            //Debug.DrawRay(transform.position, moveDirection, Color.green);
            _rigidbody.AddForce(moveDirection * shotPushForce, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            ApplySteering();
        }

        private void Update()
        {
            if (photonView.IsMine)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    if (Time.time - _lastShotTime > _reloadTime)
                    {
                        var targetDirection = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                        Shoot(cam.ScreenToWorldPoint(targetDirection));
                        
                        _lastShotTime = Time.time;
                    }
                }

                if (Debug.isDebugBuild)
                {
                    if (Input.GetKeyDown(KeyCode.Delete))
                    {
                        PhotonNetwork.Destroy(photonView);
                    }
                }
            }
        }

        private void ApplySteering()
        {
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (input == Vector2.zero)
            {
                return;
            }

            _rigidbody.AddForce(input.normalized * steeringPushForce * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}