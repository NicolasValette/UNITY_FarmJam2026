using System.Linq;
using UnityEngine;

namespace FarmJam2026
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        public GameConfigData Config;

        [Header("References in Scene")]
        public Inventaire Inventory;

        void Start()
        {
            Inventory.AddGenomeBulk(Config.GenesInInventoryAtStart);
        }
    }
}
