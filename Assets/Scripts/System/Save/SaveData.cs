using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    [Serializable]
    public class MutadexPage
    {
        public List<GenomeData> ListMutadexPages { get; set; } = new();
    }
    [Serializable]
    [CreateAssetMenu(fileName = "SaveData", menuName = "Scriptable Objects/SaveData")]
    public class SaveData : ScriptableObject
    {
        [field: SerializeField]
        public string VersionSaved { get; set; }
        [field:SerializeField]
        public List<GenomeData> SporeInInventory { get; set; } = new List<GenomeData>();

        [field: SerializeField]
        public List<GenomeData> PlantedSpores { get; set; } = new();

        [field: SerializeField]
        public List<MutadexPage> ListMutadexPages { get; set; } = new();
        
    }
}
