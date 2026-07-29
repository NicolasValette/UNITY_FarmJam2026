using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    /// <summary>
    /// Size value.
    /// </summary>
    [Serializable]
    public class SizeGene : IGene
    {
        [SerializeField] public float HorizontalScale = 1f;
        [SerializeField] public float VerticalScale = 1f;

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.HorizontalScale = HorizontalScale;
            mushroom.VerticalScale = VerticalScale;
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var sizeGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<SizeGene>().ToList();

            HorizontalScale = sizeGenes[Random.Range(0, sizeGenes.Count)].HorizontalScale;
            var roll = Random.Range(0, 1);
            if (roll < GameOptions.Instance.MutationChance)
            {
                HorizontalScale = Random.Range(GameOptions.Instance.ScaleLimits.Min, GameOptions.Instance.ScaleLimits.Max);
            }

            VerticalScale = sizeGenes[Random.Range(0, sizeGenes.Count)].VerticalScale;
            roll = Random.Range(0, 1);
            if (roll < GameOptions.Instance.MutationChance)
            {
                VerticalScale = Random.Range(GameOptions.Instance.ScaleLimits.Min, GameOptions.Instance.ScaleLimits.Max);
            }
        }
    }
}
