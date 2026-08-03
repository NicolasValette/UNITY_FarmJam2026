using FarmJam2026.Assets.Scripts.Tooltip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private bool _isMenuOpen = false;
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
        private void OnEnable()
        {
            EventManager.StartListening(EventManager.Events.OnSporeSelection, Unselect);
            EventManager.StartListening(EventManager.Events.OnUIMenuOpen, () => _isMenuOpen = true);
            EventManager.StartListening(EventManager.Events.OnUIMenuClose, () => _isMenuOpen = false);
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.OnSporeSelection, Unselect);
            EventManager.StopListening(EventManager.Events.OnUIMenuOpen, () => _isMenuOpen = true);
            EventManager.StopListening(EventManager.Events.OnUIMenuClose, () => _isMenuOpen = false);
        }
        private void Start()
        {
            _selectionIndicator.SetActive(false);   
        }

        public void EnableSelectionIndicator(bool isActivated)
        {
            if (!_isMenuOpen)
                _selectionIndicator.SetActive(isActivated);
        }


        public void UpdateColorGene()
        {
            ColorGene colorgen = (ColorGene)Spore.Genome.GenomeData.Genes.First(c => c is ColorGene);
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colorgen.Color;
        }
        private void OnMouseEnter()
        {
            EnableSelectionIndicator(true);
            var genomedata = Spore.Genome.GenomeData;
            Debug.Log("Mouse over Shroom");
            var Tip = new SporeTip()
            {
                SporeName = genomedata.GenomeName,
                GrowthTime = genomedata.Genes.OfType<GrowthGene>().First().GrowthTime,
                SporeNumber = genomedata.Genes.OfType<SporeProductionGene>().First().SporeCount,
                BiomassQuantity = genomedata.Genes.OfType<BiomassGene>().First().BiomassValue

            };
            EventManager.TriggerEvent(EventManager.Events.OnMouseEnter, Tip);
        }
        private void OnMouseExit()
        {
            if (!_isSelected)
                EnableSelectionIndicator(false);

            EventManager.TriggerEvent(EventManager.Events.OnMouseExit);
        }

        public void Select()
        {
            _isSelected = true;
            EnableSelectionIndicator(true);
        }
        private void Unselect()
        {
            _isSelected = false;
            EnableSelectionIndicator(false);
        }

    }
}
