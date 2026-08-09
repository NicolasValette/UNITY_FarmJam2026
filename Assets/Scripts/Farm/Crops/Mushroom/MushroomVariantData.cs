using System;
using UnityEngine;

namespace FarmJam2026
{
    [Serializable]
    [CreateAssetMenu(fileName = "MushVariant", menuName = "Data/Mushroom Variant")]
    public class MushroomVariantData : ScriptableObject
    {
        [SerializeField] public string VariantName;
        [SerializeField] public GameObject VariantPrefab;
        [SerializeField] public GameObject[] SporePrefabs;

        [SerializeField] public Sprite MutadexIllustrationSprite;
        [SerializeField] public Sprite MutadexTitleSprite;
        [SerializeField] public Sprite MutadexColoredSprite;
    }
}
