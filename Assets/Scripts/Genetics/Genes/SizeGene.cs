using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Size value.
    /// </summary>
    [Serializable]
    public class SizeGene : IGene
    {
        [SerializeField]
        public float Scale = 0f;

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.Scale = Scale;
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var dummy = genomes.First().Genes.OfType<SizeGene>().First();
            Scale = dummy.Scale + 1;
        }
    }
}
