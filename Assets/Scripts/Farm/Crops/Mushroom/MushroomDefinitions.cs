using UnityEngine;

namespace FarmJam2026
{
    public class MushroomDefinitions : MonoBehaviour
    {
        [SerializeField] public GameObject MushroomPrefab;

        /// <summary>
        /// This is a double entry array!!
        /// Variant xy is at index x*ENUM_COUNT+y
        /// </summary>
        [SerializeField]
        public MushroomVariantData[] MushroomVariations;


        public static MushroomDefinitions Instance { get; private set; }
        public void Awake()
        {
            Instance = this;
        }

        private void OnValidate()
        {
            Instance = this;
        }

        public MushroomVariantData GetVariationData(EBodyType primaryVariation, EBodyType secondaryVariation)
        {
            return MushroomVariations[(int)primaryVariation * (int)EBodyType.ENUM_COUNT + (int)secondaryVariation];
        }
    }
}
