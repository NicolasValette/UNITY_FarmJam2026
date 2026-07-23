using System;
using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }
    }
}
