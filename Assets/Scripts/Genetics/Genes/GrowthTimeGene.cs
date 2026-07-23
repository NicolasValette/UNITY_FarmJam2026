using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Time it takes a mushroom to reach Mature age (in seconds).
    /// </summary>
    [Serializable]
    public class GrowthTimeGene : IGene
    {
        [SerializeField]
        public float GrowthTime = 0f;

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.GrowthTime = GrowthTime;
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            throw new System.NotImplementedException();
        }
    }
}
