using System;
using System.Collections.Generic;
using System.Text;

namespace FarmJam2026
{
    public enum ItemType
    {
        Spore,
        Fertilizer
    }

    public interface IItem
    {
        ItemType Type { get; }
        public int Quantity{ get; set; }
    }
}
