using System.Linq;
using UnityEngine;

namespace FarmJam2026
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        public GameConfigData Config;
        [SerializeField]
        public GameObject HelpButton;
        [SerializeField]
        public GameObject HelpScreen;

        [Header("References in Scene")]
        public Inventaire Inventory;

        void Start()
        {
            if (SaveGame.Instance != null && SaveGame.Instance.IsGameContinue)
            {
                Debug.Log("Game Continue from save");
                SaveGame.Instance.Load();
            }
            else
            {
                Debug.Log("New Game");
                Inventory.AddGenomeBulk(Config.GenesInInventoryAtStart.Select(data => Genome.CreateGenomeFromData(data)).ToList());
                HelpButton.SetActive(false);
                HelpScreen.SetActive(true);
            }
        }
        
    }
}
