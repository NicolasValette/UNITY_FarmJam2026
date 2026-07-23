using System;
using System.Collections.Generic;
using System.Text;

namespace FarmJam2026
{
    public interface IHarvestable
    {
        List<Spore> Harvest();
    }
}
