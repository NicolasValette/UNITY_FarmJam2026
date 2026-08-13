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
        private int _drawerOpen = 0; // 0 close, 1-2-3 is drawer open;

        [SerializeField]
        private List<Transform> _invSlots;
        [SerializeField]
        private List<Transform> _invSlotsDrawer1;
        [SerializeField]
        private List<Transform> _invSlotsDrawer2;
        [SerializeField]
        private List<Transform> _invSlotsDrawer3;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Animator _animatorDrawer1;
        [SerializeField]
        private Animator _animatorDrawer2;
        [SerializeField]
        private Animator _animatorDrawer3;


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
            EventManager.StartListening<Genome>(EventManager.Events.OnTrashMushroom, AddGenome);

            if (SaveGame.Instance != null)
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
            EventManager.StopListening<Genome>(EventManager.Events.OnTrashMushroom, AddGenome);
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
        void AddSporesToInv(List<Spore> toAdd) => AddGenome(toAdd.FirstOrDefault().Genome, toAdd.Count);
        public void AddGenome(Genome genome) => AddGenome(genome, 1);
        public void AddGenome(Genome toAdd, int quantity)
        {
            
            var listSameGenome = _sporeInInventaire.FirstOrDefault(c => c.Spore.Genome == toAdd);

            if (listSameGenome is null)
            {
                var slot = _invSlots.FirstOrDefault(slot => slot.childCount == 0);
                GameObject instanceSporeInventaire = Instantiate(PrefabLibrary.Instance.SporeInventairePrefab, slot);
                instanceSporeInventaire.transform.localPosition = Vector3.zero;
                instanceSporeInventaire.GetComponent<Spore>().Genome = toAdd;


                var addedSpore = instanceSporeInventaire.GetComponent<SporeItem>();
                addedSpore.UpdateColorGene();
                addedSpore.Quantity += quantity;

                _sporeInInventaire.Add(addedSpore);
            }
            else
            {
                listSameGenome.gameObject.GetComponent<SporeItem>().Quantity += quantity;
            }
            RearrangeInventory();
        }

        //void AddSporesToInv(List<Spore> toAdd)
        //{
        //    var listSameGenome = _sporeInInventaire.FirstOrDefault(c => c.Spore.Genome == toAdd.First().Genome);
            
        //    if (listSameGenome is null)
        //    {
        //        var slot = _invSlots.FirstOrDefault(slot => slot.childCount == 0);
        //        GameObject instanceSporeInventaire = Instantiate(PrefabLibrary.Instance.SporeInventairePrefab, slot);
        //        instanceSporeInventaire.transform.localPosition = Vector3.zero;
        //        instanceSporeInventaire.GetComponent<Spore>().Genome = toAdd.FirstOrDefault().Genome;


        //        var addedSpore = instanceSporeInventaire.GetComponent<SporeItem>();
        //        addedSpore.UpdateColorGene();
        //        addedSpore.Quantity += toAdd.Count; ;

        //        _sporeInInventaire.Add(addedSpore);
        //    }
        //    else
        //    {
        //        listSameGenome.gameObject.GetComponent<SporeItem>().Quantity += toAdd.Count;
        //    }
        //}
      

        void RemoveFromInv(Genome genome)
        {
            var spore = _sporeInInventaire.FirstOrDefault(c => c.Spore.Genome == genome);
            if (spore != null && spore.Quantity > 0)
                spore.Quantity--;
            if(spore != null && spore.Quantity <= 0)
            {
                _sporeInInventaire.Remove(spore);
                spore.transform.SetParent(null);
                Destroy(spore.gameObject);
            }
            RearrangeInventory();
        }
        private void RearrangeInventory()
        {
            for (int i = 0; i < _invSlots.Count; i++)
            {
                if (_invSlots[i].transform.childCount == 0)
                {
                    continue;
                }
                var slot = _invSlots.FirstOrDefault(slot => slot.childCount == 0);
                int ind = _invSlots.IndexOf(slot);
                if (ind < i)
                {
                    Transform child = _invSlots[i].GetChild(0);
                    child.SetParent(slot);
                    child.localPosition = Vector2.zero;
                }
            }
        }
        private void AddBiomass(int amount)
        {
            _totalBiomass += amount;
            _biomassText.text = $"Biomass: {_totalBiomass.ToString()}";
        }
        public void ToggleInventory()
        {
            if (DragAndDropHolderFSM.Instance.CurrentState is not IdleState) return;
            if (_isInventoryOpen)
            {
                if (_drawerOpen == 1)
                {
                    _animatorDrawer1.SetTrigger("CloseDrawer1");
                    _animatorDrawer2.SetTrigger("OpenDrawer2");
                    _drawerOpen = 2;
                }
                else if (_drawerOpen == 2)
                {
                    _animatorDrawer2.SetTrigger("CloseDrawer2");
                    _animatorDrawer3.SetTrigger("OpenDrawer3");
                    _drawerOpen = 3;
                }
                else if (_drawerOpen == 3)
                {
                    _animatorDrawer3.SetTrigger("CloseDrawer3");
                    _drawerOpen = 0;
                    _isInventoryOpen = false;
                }
            }
            else
            {
                _animatorDrawer1.SetTrigger("OpenDrawer1");
                _isInventoryOpen = true;
                _drawerOpen = 1;
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
