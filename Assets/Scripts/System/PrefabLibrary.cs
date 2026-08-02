using UnityEngine;

namespace FarmJam2026
{
    public class PrefabLibrary : MonoBehaviour
    {
        [Header("Game PREFABS")]
        public GameObject MushroomPrefab;
        public GameObject SporeInventairePrefab;
        [Header("UI PREFABS")]
        [field: SerializeField]
        public GameObject MutadexColorPagePrefab { get; private set; }
        private static PrefabLibrary _instance;
        public static PrefabLibrary Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }
    }
}
