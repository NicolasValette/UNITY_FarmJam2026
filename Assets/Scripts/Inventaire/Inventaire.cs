using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

namespace FarmJam2026
{
    //je repasse dessus des que possible parceque c'est moche a souhait mais il est tard et je taf demain :c
    public class Inventaire : MonoBehaviour, ISaveable
    {
        public string Name { get; } = "Inventaire";
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
        private bool _isInventoryOpen = false;

        [SerializeField]
        private List<Transform> _invSlots;
        [SerializeField]
        private Animator _animator;
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
            _biomassText.text = $"Biomass: {_totalBiomass.ToString()}";
        }
        private void OnEnable()
        {
            EventManager.StartListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
            EventManager.StartListening<Genome>(EventManager.Events.OnPlant, RemoveFromInv);
            EventManager.StartListening<Genome>(EventManager.Events.OnAddToBlender, RemoveFromInv);
            EventManager.StartListening<Genome>(EventManager.Events.OnTrash, RemoveFromInv);
            EventManager.StartListening<Genome>(EventManager.Events.OnBlend, AddGenome);
            EventManager.StartListening<int>(EventManager.Events.OnMushroomDecay, AddBiomass);
            EventManager.StartListening(EventManager.Events.OnOpenCloseInventory, ToggleInventory);

            SaveGame.Instance.RegisterSaveable(this);
        }

        private void OnDisable()
        {
            EventManager.StopListening<List<Spore>>(EventManager.Events.OnHarvest, AddSporesToInv);
            EventManager.StopListening<Genome>(EventManager.Events.OnPlant, RemoveFromInv);
            EventManager.StopListening<Genome>(EventManager.Events.OnAddToBlender, RemoveFromInv);
            EventManager.StopListening<Genome>(EventManager.Events.OnTrash, RemoveFromInv);
            EventManager.StopListening<Genome>(EventManager.Events.OnBlend, AddGenome);
            EventManager.StopListening<int>(EventManager.Events.OnMushroomDecay, AddBiomass);
            EventManager.StopListening(EventManager.Events.OnOpenCloseInventory, ToggleInventory);
            
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
            _biomassText.text = $"Biomass: {_totalBiomass.ToString()}";
        }
        private void ToggleInventory()
        {
            if (_isInventoryOpen)
            {
                _animator.SetTrigger("CloseInventory");
                _isInventoryOpen = false;
            }
            else
            {
                _animator.SetTrigger("OpenInventory");
                _isInventoryOpen = true;
            }
        }
        private void CloseInventory()
        {
            
        }

        public void Save(ref SaveData data)
        {
            foreach (var item in _sporeInInventaire)
            {
                for (int i = 0; i < item.Quantity; i++)
                    data.SporeInInventory.Add(item.Spore.Genome.GenomeData);
            }
            Debug.Log("[SAVE] INVENTORY SAVED !");
        }

        public void Load(SaveData data)
        {
            var list = data.SporeInInventory.Select(x => new Genome { GenomeData = x }).ToList();
            AddGenomeBulk(list);
            Debug.Log("[LOAD] INVENTORY LOADED !");
        }
    }
}
