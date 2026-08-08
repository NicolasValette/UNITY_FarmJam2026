using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    [Serializable]
    public class NoiseGene : IGene
    {
        [SerializeField]
        public ENoiseType NoiseType;

        public bool Equals(IGene other)
        {
            var noiseOther = other as NoiseGene;
            if (noiseOther == null)
                return false;

            return EqualityComparer<ENoiseType>.Default.Equals(NoiseType, noiseOther.NoiseType);
        }

        public void ExpressOn(Mushroom mushroom)
        {
            //TODO express noise
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var noiseGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<NoiseGene>().ToList();

            NoiseType = noiseGenes[Random.Range(0, noiseGenes.Count)].NoiseType;
            var roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                if (NoiseType == ENoiseType.Silent)
                    NoiseType++;
                else if (NoiseType == ENoiseType.Chant)
                    NoiseType--;
                else
                {
                    roll = Random.Range(0f, 1f);
                    if (roll < 0.5f)
                        NoiseType++;
                    else NoiseType--;
                }
            }
        }
    }
}