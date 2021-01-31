using UnityEngine;

namespace PlanetNein.Runtime.Gameplay
{
    public class SpriteSwitcher : MonoBehaviour
    {
        [SerializeField] private Sprite[] _faceSprites;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _sprites;
        private int _planetID;

        private void Awake()
        {
            _planetID = Random.Range(0, _sprites.Length);
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = _sprites[_planetID];
        }

        public void ShowFace()
        {
            _spriteRenderer.sprite = _faceSprites[_planetID];
        }
    }
}