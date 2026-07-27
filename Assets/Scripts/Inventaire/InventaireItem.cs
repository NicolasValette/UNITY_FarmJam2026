using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace FarmJam2026
{
    public class InventaireItem : MonoBehaviour
    {
        public Spore item;
        private int Quantity = 0;

        [Header("UI Links")]
        [SerializeField]
        private TextMeshPro QuantityText;

        public void UpdateCount(int count)
        {
            Quantity += count;
            QuantityText.text = Quantity.ToString();
        }
    }
}
