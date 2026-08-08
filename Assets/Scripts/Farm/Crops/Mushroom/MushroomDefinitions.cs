using UnityEngine;

namespace FarmJam2026
{
    public class MushroomDefinitions : MonoBehaviour
    {
        [SerializeField] public GameObject MushroomPrefab;

        [SerializeField] public MushroomVariantData[] MushroomVariations;


        public static MushroomDefinitions Instance { get; private set; }
        public void Awake()
        {
            Instance = this;
        }
        private void OnValidate()
        {
            Instance = this;
        }
    }
}
