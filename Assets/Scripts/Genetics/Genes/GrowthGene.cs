using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public bool Equals(IGene other)
        {
            var growthOther = other as GrowthGene;
            if (growthOther == null)
                return false;

            return EqualityComparer<float>.Default.Equals(GrowthTime, growthOther.GrowthTime)
                && EqualityComparer<float>.Default.Equals(LifeSpan, growthOther.LifeSpan);
        }

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.GrowthTime = GrowthTime;
            mushroom.LifeSpan = LifeSpan;
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var growthGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<GrowthGene>().ToList();

            GrowthTime = growthGenes[Random.Range(0, growthGenes.Count)].GrowthTime;
            var roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                GrowthTime = Random.Range(GameOptions.Instance.GrowthTimeLimits.Min, GameOptions.Instance.GrowthTimeLimits.Max);
            }

            LifeSpan = growthGenes[Random.Range(0, growthGenes.Count)].LifeSpan;
            roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                LifeSpan = Random.Range(GameOptions.Instance.LifeSpanLimits.Min, GameOptions.Instance.LifeSpanLimits.Max);
            }
        }
    }
}
