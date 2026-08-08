using System;

namespace FarmJam2026
{
    [Serializable]
    public class GlowGene : AccessoryGene
    {
        public override void ExpressOn(Mushroom mushroom)
        {
            mushroom.SetGlow(IsActive);
        }
    }
}