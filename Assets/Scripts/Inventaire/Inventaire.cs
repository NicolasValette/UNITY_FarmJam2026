using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

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
        [SerializeField]
        private TMP_Text _biomassText;
        private int _totalBiomass;

        [SerializeField]
        private List<Transform> _invSlots;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            //_nbcell = nbCell;
            //_gridSizeX = -GridMargin;
            //_gridSizeY = GridMargin;
        }
        private void Start()
        {
            _totalBiomass = 0;
            _biomassText.text = _totalBiomass.ToString();
        }
        private void OnEnable()
        {
            EventManager.StartListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
            EventManager.StartListening<Genome>(EventManager.Events.OnPlant, RemoveFromInv);
            EventManager.StartListening<Genome>(EventManager.Events.OnAddToBlender, RemoveFromInv);
            EventManager.StartListening<Genome>(EventManager.Events.OnBlend, AddGenome);
            EventManager.StartListening<int>(EventManager.Events.OnMushroomDecay, AddBiomass);
        }

        private void OnDisable()
        {
            EventManager.StopListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
            EventManager.StopListening<Genome>(EventManager.Events.OnPlant, RemoveFromInv);
            EventManager.StopListening<Genome>(EventManager.Events.OnAddToBlender, RemoveFromInv);
            EventManager.StopListening<Genome>(EventManager.Events.OnBlend, AddGenome);
            EventManager.StopListening<int>(EventManager.Events.OnMushroomDecay, AddBiomass);
        }

        /// <summary>
        /// Hackey hackey ;P
        /// </summary>
        /// <param name="toAdd">They may be different.</param>
        public void AddGenomeBulk(List<Genome> toAdd)
        {
            foreach (var genome in toAdd)
            {
                AddGenome(genome);
            }
        }
        public void AddGenome(Genome toAdd)
        {
            
            var listSameGenome = _sporeInInventaire.FirstOrDefault(c => c.Spore.Genome == toAdd);

            if (listSameGenome is null)
            {
                //float addedPosX = (_sporeInInventaire.Count % nbCell) / _nbcell;
                //float addedPosY = (_sporeInInventaire.Count / nbCell) / _nbcell;
                //Vector2 newpos = new Vector2(_gridSizeX + addedPosX, _gridSizeY - addedPosY);
                var slot = _invSlots.FirstOrDefault(slot => slot.childCount == 0);
                GameObject instanceSporeInventaire = Instantiate(PrefabLibrary.Instance.SporeInventairePrefab, slot);
                instanceSporeInventaire.transform.localPosition = Vector3.zero;
                instanceSporeInventaire.GetComponent<Spore>().Genome = toAdd;


                var addedSpore = instanceSporeInventaire.GetComponent<SporeItem>();
                addedSpore.UpdateColorGene();
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
            var listSameGenome = _sporeInInventaire.FirstOrDefault(c => c.Spore.Genome == toAdd.First().Genome);
            
            if (listSameGenome is null)
            {
                //float addedPosX = (_sporeInInventaire.Count % nbCell) / _nbcell;
                //float addedPosY = (_sporeInInventaire.Count / nbCell) / _nbcell;
                //Vector2 newpos = new Vector2(_gridSizeX + addedPosX, _gridSizeY - addedPosY);
                var slot = _invSlots.FirstOrDefault(slot => slot.childCount == 0);
                GameObject instanceSporeInventaire = Instantiate(PrefabLibrary.Instance.SporeInventairePrefab, slot);
                instanceSporeInventaire.transform.localPosition = Vector3.zero;
                instanceSporeInventaire.GetComponent<Spore>().Genome = toAdd.FirstOrDefault().Genome;


                var addedSpore = instanceSporeInventaire.GetComponent<SporeItem>();
                addedSpore.UpdateColorGene();
                addedSpore.Quantity += toAdd.Count; ;

                _sporeInInventaire.Add(addedSpore);
            }
            else
            {
                listSameGenome.gameObject.GetComponent<SporeItem>().Quantity += toAdd.Count;
            }
        }

        void RemoveFromInv(Genome genome)
        {
            var spore = _sporeInInventaire.FirstOrDefault(c => c.Spore.Genome == genome);
            if (spore != null && spore.Quantity > 0)
                spore.Quantity--;
            if(spore != null && spore.Quantity <= 0)
            {
                _sporeInInventaire.Remove(spore);
                Destroy(spore.gameObject);
            }
        }
        private void AddBiomass(int amount)
        {
            _totalBiomass += amount;
            _biomassText.text = _totalBiomass.ToString();
        }
    }
}
