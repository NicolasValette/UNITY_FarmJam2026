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
        [SerializeField] public MushroomVariantData VariantData;

        public bool Equals(IGene other)
        {
            var vOther = other as VariantGene;
            if (vOther == null)
                return false;

            return EqualityComparer<MushroomVariantData>.Default.Equals(VariantData, vOther.VariantData);
        }

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.ApplyVariant(VariantData);
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var varGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<VariantGene>().ToList();
            VariantData = varGenes[Random.Range(0, varGenes.Count)].VariantData;
        }
    }
}