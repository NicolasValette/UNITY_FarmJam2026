using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    public class SpriteLibrary : MonoBehaviour
    {
        [field:SerializeField]
        public Sprite TriangleTypeSprite { get; private set; }
        [field: SerializeField]
        public Sprite CircleTypeSprite {  get; private set; }
        [field: SerializeField]
        public Sprite LosangeTypeSprite { get; private set; }

        private static SpriteLibrary _instance;
        public static SpriteLibrary Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

    }
}
