using UnityEngine;
using UnityEngine.UI;

namespace PlanetNein.Runtime.UI
{
    [RequireComponent(typeof(RawImage))]
    public class RawImagePanner : MonoBehaviour
    {
        private RawImage _rawImage;
        private Vector2 _scrollDirection;
        [SerializeField] private Vector2 _scrollSpeed;

        private void Awake()
        {
            _rawImage = GetComponent<RawImage>();
            _scrollDirection = Random.insideUnitCircle * _scrollSpeed;
        }

        private void Update()
        {
            _rawImage.uvRect = new Rect(Time.time * _scrollDirection, Vector2.one);
        }
    }
}