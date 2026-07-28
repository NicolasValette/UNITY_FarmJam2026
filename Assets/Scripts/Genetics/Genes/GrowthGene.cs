using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Time it takes a mushroom to reach Mature, and Death.
    /// </summary>
    [Serializable]
    public class GrowthGene : IGene
    {
        [SerializeField]
        public float GrowthTime = 0f;
        [SerializeField]
        public float LifeSpan = 0f;

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.GrowthTime = GrowthTime;
            mushroom.LifeSpan = LifeSpan;
        }

        public void PerformHybridization(List<GenomeData> genomes)
        {
            var dummy = genomes.First().Genes.OfType<GrowthGene>().First();
            GrowthTime = dummy.GrowthTime + 1;
            LifeSpan = dummy.LifeSpan + 1;
        }
    }
}
