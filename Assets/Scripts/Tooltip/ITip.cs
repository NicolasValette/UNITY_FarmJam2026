

using UnityEngine;

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
        string GetMessage();
        Vector2 Position { get; set; }
    }
}
