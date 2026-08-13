using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FarmJam2026
{
    public class SporeTip : ITip
    {
        public string SporeName;
        public float GrowthTime;
        public int SporeNumber;
        public int BiomassQuantity;
        public TipType type { get => TipType.Spore; }
        public Vector2 Position { get; set; }

        public string GetMessage()
        {
            return $"{SporeName}\nGrowth time: {GrowthTime.ToString(".0")}\nSpore count: {SporeNumber}\n";// Biomass Value: {BiomassQuantity}";
        }
    }
}
