using Photon.Pun;
using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class LocalPlayerMarker : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(GetComponentInParent<PhotonView>().IsMine);
        }
    }
}