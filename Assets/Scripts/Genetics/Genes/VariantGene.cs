using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026.Assets.Scripts.Genetics.Genes
{
    [Serializable]
    public class VariantGene : IGene
    {
        [SerializeField] public EBodyType PrimaryVariation;
        [SerializeField] public EBodyType SecondaryVariation;

        public bool Equals(IGene other)
        {
            var vOther = other as VariantGene;
            if (vOther == null)
                return false;

            return EqualityComparer<EBodyType>.Default.Equals(PrimaryVariation, vOther.PrimaryVariation)
                && EqualityComparer<EBodyType>.Default.Equals(SecondaryVariation, vOther.SecondaryVariation);
        }

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.ApplyVariant(MushroomDefinitions.Instance.GetVariationData(PrimaryVariation, SecondaryVariation));
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var varGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<VariantGene>().ToList();

            var allVariants = varGenes.Select(vg => vg.PrimaryVariation).ToList();
            allVariants.AddRange(varGenes.Select(vg => vg.SecondaryVariation));

            var rand = Random.Range(0, allVariants.Count);
            PrimaryVariation = allVariants[rand];
            allVariants.RemoveAt(rand);
            SecondaryVariation = allVariants[Random.Range(0, allVariants.Count)];

            var roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                PrimaryVariation = (EBodyType)Random.Range(0, 4);
            }

            roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                SecondaryVariation = (EBodyType)Random.Range(0, 4);
            }
        }
    }
}