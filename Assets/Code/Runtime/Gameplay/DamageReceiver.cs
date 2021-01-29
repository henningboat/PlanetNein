using Photon.Pun;

namespace PlanetNein.Runtime.Gameplay
{
    public class DamageReceiver : MonoBehaviourPun
    {
        public bool IsPlayer { get; private set; }

        private void Awake()
        {
            IsPlayer = GetComponent<PlayerControl>() != null;
        }

        public void Damage()
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}