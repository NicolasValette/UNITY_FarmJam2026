using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
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
