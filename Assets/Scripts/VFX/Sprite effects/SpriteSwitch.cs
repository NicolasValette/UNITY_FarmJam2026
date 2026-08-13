using Unity.VisualScripting;
using UnityEngine;

namespace FarmJam2026
{
    public class SpriteSwitch : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        private bool _hasSwitch = false;

        public void SwitchSprite()
        {
            _hasSwitch = !_hasSwitch;
            _animator.SetBool("IsOpen", _hasSwitch);
        }
    }
}
