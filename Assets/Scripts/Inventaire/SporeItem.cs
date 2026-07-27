using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace FarmJam2026
{
    public class SporeItem : MonoBehaviour,IItem
    {

        public Spore Spore;
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
        private int _quantity;

        [SerializeField]
        private TextMeshPro _quantityText;

        public ItemType Type { get => ItemType.Spore; }


    }
}
