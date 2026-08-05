using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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
            var biomassGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<BiomassGene>().ToList();

            BiomassValue = biomassGenes[Random.Range(0, biomassGenes.Count)].BiomassValue;
            var roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                BiomassValue = Random.Range(GameOptions.Instance.BiomassValueLimits.Min, GameOptions.Instance.BiomassValueLimits.Max);
            }
        }
    }
}
