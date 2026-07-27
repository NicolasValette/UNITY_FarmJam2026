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
        List<SporeItem> _sporeInInventaire = new List<SporeItem>();

        float _gridSizeX, _gridSizeY;
        [SerializeField]
        float GridMargin;
        [SerializeField]
        public int nbCell;
        float _nbcell;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _nbcell = nbCell;
            _gridSizeX = -GridMargin;
            _gridSizeY = GridMargin;
        }
        private void OnEnable()
        {
            EventManager.StartListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
            EventManager.StartListening<GenomeData>(EventManager.Events.OnPlant, PlantedSpore);
        }

        private void OnDisable()
        {
            EventManager.StopListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
            EventManager.StopListening<GenomeData>(EventManager.Events.OnPlant, PlantedSpore);
        }

        /// <summary>
        /// Hackey hackey ;P
        /// </summary>
        /// <param name="toAdd">They may be different.</param>
        public void AddGenomeBulk(List<GenomeData> toAdd)
        {
            foreach (var genome in toAdd)
            {
                AddGenome(genome);
            }
        }
        public void AddGenome(GenomeData toAdd)
        {
            var listSameGenome = _sporeInInventaire.FirstOrDefault(c => c.Spore.GenomeToGrow == toAdd);

            if (listSameGenome is null)
            {
                float addedPosX = (_sporeInInventaire.Count % nbCell) / _nbcell;
                float addedPosY = (_sporeInInventaire.Count / nbCell) / _nbcell;
                Vector2 newpos = new Vector2(_gridSizeX + addedPosX, _gridSizeY - addedPosY);

                GameObject instanceSporeInventaire = Instantiate(PrefabLibrary.Instance.SporeInventairePrefab, GridInventaire.transform);
                instanceSporeInventaire.transform.localPosition = newpos;
                instanceSporeInventaire.GetComponent<Spore>().GenomeToGrow = toAdd;


                var addedSpore = instanceSporeInventaire.GetComponent<SporeItem>();
                addedSpore.Quantity++;

                _sporeInInventaire.Add(addedSpore);
            }
            else
            {
                listSameGenome.gameObject.GetComponent<SporeItem>().Quantity++;
            }
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

        void PlantedSpore(GenomeData planted)
        {

            var spore = _sporeInInventaire.FirstOrDefault(c => c.Spore.GenomeToGrow == planted);
            if (spore != null && spore.Quantity > 0)
                spore.Quantity--;
        }
    }
}
