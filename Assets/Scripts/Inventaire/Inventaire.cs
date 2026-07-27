using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FarmJam2026
{
    //je repasse dessus des que possible parceque c'est moche a souhait mais il est tard et je taf demain :c
    public class Inventaire : MonoBehaviour
    {
        public GameObject GridInventaire;
        List<SporeItem> _sporeInInventaire;

        float _gridSizeX, _gridSizeY;
        [SerializeField]
        float GridMargin;
        [SerializeField]
        public int nbCell;
        float _nbcell;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _nbcell = nbCell;
            _sporeInInventaire = new List<SporeItem>();

            _gridSizeX = -GridMargin;
            _gridSizeY = GridMargin;
        }
        private void OnEnable()
        {
            EventManager.StartListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
            EventManager.StartListening<Genome>(EventManager.Events.OnPlant, PlantedSpore);
        }

        private void OnDisable()
        {
            EventManager.StopListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
            EventManager.StopListening<Genome>(EventManager.Events.OnPlant, PlantedSpore);
        }

        void AddSporesToInv(List<Spore> toAdd)
        {
            var listSameGenome = _sporeInInventaire.FirstOrDefault(c => c.Spore.GenomeToGrow == toAdd.First().GenomeToGrow);
            
            if (listSameGenome is null)
            {
                float addedPosX = (_sporeInInventaire.Count % nbCell) / _nbcell;
                float addedPosY = (_sporeInInventaire.Count / nbCell) / _nbcell;
                Vector2 newpos = new Vector2(_gridSizeX + addedPosX, _gridSizeY - addedPosY);
           
                GameObject instanceSporeInventaire = Instantiate(PrefabLibrary.Instance.SporeInventairePrefab, GridInventaire.transform);
                instanceSporeInventaire.transform.localPosition = newpos;
                instanceSporeInventaire.GetComponent<Spore>().GenomeToGrow = toAdd.FirstOrDefault().GenomeToGrow;


                var addedSpore = instanceSporeInventaire.GetComponent<SporeItem>();
                addedSpore.Quantity += toAdd.Count; ;

                _sporeInInventaire.Add(addedSpore);
            }
            else
            {
                listSameGenome.gameObject.GetComponent<SporeItem>().Quantity += toAdd.Count;
            }
        }

        void PlantedSpore(Genome planted)
        {

            var spore = _sporeInInventaire.FirstOrDefault(c => c.Spore.GenomeToGrow == planted);
            if (spore != null && spore.Quantity > 0)
                spore.Quantity--;
        }
    }
}
