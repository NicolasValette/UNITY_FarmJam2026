using System;
using System.Collections.Generic;
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

        public void PerformHybridization(List<Genome> genomes)
        {
            throw new System.NotImplementedException();
        }
    }
}
