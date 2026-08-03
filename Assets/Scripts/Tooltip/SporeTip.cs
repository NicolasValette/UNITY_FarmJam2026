using System;
using System.Collections.Generic;
using System.Text;

namespace FarmJam2026
{
    public class SporeTip : ITip
    {
        public string SporeName;
        public float GrowthTime;
        public int SporeNumber;
        public int BiomassQuantity;
        public TipType type { get => TipType.Spore; }
    }
}
