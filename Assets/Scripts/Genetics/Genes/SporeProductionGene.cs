using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public bool Equals(IGene other)
        {
            var sporeOther = other as SporeProductionGene;
            if (sporeOther == null)
                return false;

            return EqualityComparer<float>.Default.Equals(SporeGrowthTime, sporeOther.SporeGrowthTime)
                && EqualityComparer<int>.Default.Equals(SporeCount, sporeOther.SporeCount);
        }

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.SporeGrowthTime = SporeGrowthTime;
            mushroom.SporeCount = SporeCount;
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var productionGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<SporeProductionGene>().ToList();

            SporeGrowthTime = productionGenes[Random.Range(0, productionGenes.Count)].SporeGrowthTime;
            var roll = Random.Range(0, 1);
            if (roll < GameOptions.Instance.MutationChance)
            {
                SporeGrowthTime = Random.Range(GameOptions.Instance.SporeGrowthLimits.Min, GameOptions.Instance.SporeGrowthLimits.Max);
            }

            SporeCount = productionGenes[Random.Range(0, productionGenes.Count)].SporeCount;
            roll = Random.Range(0, 1);
            if (roll < GameOptions.Instance.MutationChance)
            {
                SporeCount = Random.Range(GameOptions.Instance.SporeCountLimits.Min, GameOptions.Instance.SporeCountLimits.Max);
            }
        }
    }
}
