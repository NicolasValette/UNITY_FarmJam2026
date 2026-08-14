using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;

namespace FarmJam2026
{
    public class SporeItem : MonoBehaviour,IItem
    {
        #region Serialized Field
        [SerializeField]
        private TextMeshPro _quantityText;
        [SerializeField]
        private GameObject _selectionIndicator;
        #endregion
        public Spore Spore;
        private int _quantity;
        private bool _isSelected = false;
        public ItemType Type { get => ItemType.Spore; }
        public int Quantity 
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                _quantityText.text = _quantity.ToString();
            }
        }

        [HideInInspector]
        public Inventaire InventaireRef;
        private bool _isPlantedCache = false;
        private bool IsPlanted
        {
            get
            {
                if (_isPlantedCache) return true;

                _isPlantedCache = InventaireRef.PlantedSpores.Contains(Spore.Genome.GenomeData);
                return _isPlantedCache;
            }
        }


        private void OnEnable()
        {
            EventManager.StartListening(EventManager.Events.OnSporeSelection, Unselect);
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.OnSporeSelection, Unselect);
        }
        private void Start()
        {
            _selectionIndicator.SetActive(false);   
        }

   


        public void UpdateColorGene()
        {
            ColorGene colorgen = (ColorGene)Spore.Genome.GenomeData.Genes.First(c => c is ColorGene);
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colorgen.Color;
        }
        private void OnMouseEnter()
        {
            var genomedata = Spore.Genome.GenomeData;
            UnityEngine.Debug.Log("Mouse over Shroom");
            var Tip = new SporeTip()
            {
                GrowthTime = genomedata.Genes.OfType<GrowthGene>().First().GrowthTime,
                LifeTime = genomedata.Genes.OfType<GrowthGene>().First().LifeSpan,
                SporeNumber = genomedata.Genes.OfType<SporeProductionGene>().First().SporeCount,
                BiomassQuantity = genomedata.Genes.OfType<BiomassGene>().First().BiomassValue,
                GenomeData = genomedata,
                IsPlanted = IsPlanted,
                Position = transform.position
            };
            EventManager.TriggerEvent(EventManager.Events.OnMouseEnter, Tip);
        }
        private void OnMouseExit()
        {
            if (!_isSelected)

            EventManager.TriggerEvent(EventManager.Events.OnMouseExit);
        }

        public void Select()
        {
            _isSelected = true;
        }
        private void Unselect()
        {
            _isSelected = false;
        }

    }
}
