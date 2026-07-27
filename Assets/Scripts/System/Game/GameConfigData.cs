using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    [CreateAssetMenu(fileName = "Configuration", menuName = "Data/Game Config")]
    public class GameConfigData : ScriptableObject
    {
        [SerializeField]
        public List<GenomeData> GenesInInventoryAtStart;
    }
}
