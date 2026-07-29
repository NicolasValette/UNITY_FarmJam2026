using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Spore growth time, and produced spore count.
    /// </summary>
    [Serializable]
    public class SporeProductionGene : IGene
    {
        [SerializeField]
        public float SporeGrowthTime = 0f;

        [SerializeField]
        public int SporeCount = 0;

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.SporeGrowthTime = SporeGrowthTime;
            mushroom.SporeCount = SporeCount;
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var dummy = genomes.First().Genes.OfType<SporeProductionGene>().First();
            SporeGrowthTime = dummy.SporeGrowthTime + 1;
            SporeCount = 1;
        }
    }
}
