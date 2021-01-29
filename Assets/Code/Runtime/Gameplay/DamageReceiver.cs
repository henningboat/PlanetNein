using Photon.Pun;

namespace PlanetNein.Runtime.Gameplay
{
    public class DamageReceiver:MonoBehaviourPun
    {
        public void Damage()
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}