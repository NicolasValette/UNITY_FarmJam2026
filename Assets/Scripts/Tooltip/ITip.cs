using System;
using System.Collections.Generic;
using System.Text;

namespace FarmJam2026
{
    public enum TipType
    {
        Shroom,
        Spore,
        Item
    }
    interface ITip
    {
        public TipType type
        {
            get;
        }
    }
}
