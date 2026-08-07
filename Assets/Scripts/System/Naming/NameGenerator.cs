using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    public class NameGenerator : MonoBehaviour
    {
        public static NameGenerator Instance { get; private set;  }
        public void OnEnable()
        {
            Instance = this;
        }
        public void OnValidate()
        {
            Instance = this;
        }

        [SerializeField]
        public List<string> NameParts = new List<string>();

        [SerializeField]
        public List<string> Suffixes = new List<string>();

        public string GenerateRandomName()
        {
            var name1 = NameParts[Random.Range(0, NameParts.Count)];
            var name2 = NameParts[Random.Range(0, NameParts.Count)];
            var suffix1 = Suffixes[Random.Range(0, Suffixes.Count)];
            var suffix2 = Suffixes[Random.Range(0, Suffixes.Count)];
            return $"{name1}{suffix1} {name2}{suffix2}";
        }
    }
}
