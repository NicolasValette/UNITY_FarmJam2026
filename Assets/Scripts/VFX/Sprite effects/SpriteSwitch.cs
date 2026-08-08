using Unity.VisualScripting;
using UnityEngine;

namespace FarmJam2026
{
    public class SpriteSwitch : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private Sprite _startingSprite;
        [SerializeField]
        private Sprite _otherSprite;

        private bool _hasSwitch = false;

        public void SwitchSprite()
        {
            _spriteRenderer.sprite = (_hasSwitch) ? _startingSprite : _otherSprite;
            _hasSwitch = !_hasSwitch;
        }
    }
}
