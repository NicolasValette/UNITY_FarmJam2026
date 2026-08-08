using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    public class MushroomVariant : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer PrincipalColorSprite;
        [SerializeField] public GameObject SporeSlotsParent;
        [SerializeField] public GameObject GlowAccessory;

        public List<Transform> SporeSlots { get; private set; } = new List<Transform>();

        private void Awake()
        {
            var sporeSlotCount = SporeSlotsParent.transform.childCount;
            for (int i = 0; i < sporeSlotCount; i++)
            {
                SporeSlots.Add(SporeSlotsParent.transform.GetChild(i));
            }
        }
    }
}
