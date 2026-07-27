using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FarmJam2026
{
    //je repasse dessus des que possible parceque c'est moche a souhait mais il est tard et je taf demain :c
    public class Inventaire : MonoBehaviour
    {
        public GameObject SporeInventaire;
        public GameObject GridInventaire;
        List<Spore> _sporeInInventaire;



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
            _sporeInInventaire = new List<Spore>();

            _gridSizeX = -GridMargin;
            _gridSizeY = GridMargin;
        }
        private void OnEnable()
        {
            EventManager.StartListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
        }

        private void OnDisable()
        {
            EventManager.StopListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
        }

        void AddSporesToInv(List<Spore> toAdd)
        {
            var listSameGenome = _sporeInInventaire.Where(c => c.GenomeToGrow == toAdd.First().GenomeToGrow).FirstOrDefault();
            if (listSameGenome is null)
            {
                float addedPosX = (_sporeInInventaire.Count % nbCell) / _nbcell;
                float addedPosY = (_sporeInInventaire.Count / nbCell) / _nbcell;
                Vector2 newpos = new Vector2(_gridSizeX + addedPosX, _gridSizeY - addedPosY);
           
                GameObject instanceSporeInventaire = Instantiate(SporeInventaire, GridInventaire.transform);
                instanceSporeInventaire.transform.localPosition = newpos;
                instanceSporeInventaire.GetComponent<InventaireItem>().UpdateCount(toAdd.Count);
                var addedSpore = instanceSporeInventaire.GetComponent<Spore>();
                addedSpore.GenomeToGrow = toAdd.FirstOrDefault().GenomeToGrow;
                _sporeInInventaire.Add(addedSpore);
            }
            else
            {
                listSameGenome.gameObject.GetComponent<InventaireItem>().UpdateCount(toAdd.Count);
            }
        }

        public void StartPlanting(Spore toPlant)
        {

        }
    }
}
