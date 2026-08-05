using System;
using System.Collections.Generic;
using System.Text;

namespace FarmJam2026.Assets.Scripts.Tooltip
{
    public class MushroomTip : ITip
    {
        public float LifeLeft;
        public string ShroomName;
       public TipType type { get => TipType.Shroom; }
    }
}
