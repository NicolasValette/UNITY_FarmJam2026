using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Biomass player get when this mushroom decay
    /// </summary>
    [Serializable]
    public class BiomassGene : IGene
    {
        [SerializeField]
        public int BiomassValue;
        public bool Equals(IGene other)
        {
            var biomassOther = other as BiomassGene;
            return BiomassValue == biomassOther.BiomassValue;
        }

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.BiomassValue = BiomassValue;
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            throw new System.NotImplementedException();
        }

       
    }
}
