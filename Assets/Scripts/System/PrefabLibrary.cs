using Unity.VisualScripting;
using UnityEngine;

namespace FarmJam2026
{
    public class PrefabLibrary : MonoBehaviour
    {
        private static PrefabLibrary _instance;
        public static PrefabLibrary Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }

        public GameObject MushroomPrefab;
    }
}
