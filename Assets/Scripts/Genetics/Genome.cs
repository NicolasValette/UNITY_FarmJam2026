using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmJam2026
{
    [Serializable]
    public class Genome
    {
        [SerializeField] public GenomeData GenomeData;

        public static Genome CreateGenomeFromData(GenomeData data)
        {
            return new Genome { GenomeData = data };
        }

        public static Genome CreateHybrid(List<Genome> genomes)
        {
            var hybridData = ScriptableObject.CreateInstance<GenomeData>();
            var geneGroups = genomes
                .SelectMany(g => g.GenomeData.Genes)
                .GroupBy(gene => gene.GetType())
                .ToDictionary(group => group.Key, group => group.ToList());

            foreach (var geneGroup in geneGroups)
            {
                var geneType = geneGroup.Key;
                var genes = geneGroup.Value;

                var newGene = (IGene)Activator.CreateInstance(geneType);
                newGene.PerformHybridization(genomes);
                hybridData.Genes.Add(newGene);
            }

            return new Genome { GenomeData = hybridData };
        }

        public void ExpressOn(Mushroom mushroom)
        {
            if (GenomeData == null)
                return;

            foreach (IGene gene in GenomeData.Genes)
            {
                gene.ExpressOn(mushroom);
            }
        }
    }
}
