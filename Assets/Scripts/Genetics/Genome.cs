using System;
using System.Collections.Generic;
using System.Linq;
using FarmJam2026.Assets.Scripts.Genetics.Genes;
using UnityEngine;

namespace FarmJam2026
{
    [Serializable]
    public class Genome : IEquatable<Genome>
    {
        [SerializeField] public GenomeData GenomeData;

        #region Equatable
        public bool Equals(Genome other)
        {
            if (GenomeData.Genes.Count != other.GenomeData.Genes.Count) return false;
            for (int i = 0; i < GenomeData.Genes.Count; ++i)
            {
                var gene = GenomeData.Genes[i];
                var otherGene = other.GenomeData.Genes[i];
                if (!gene.Equals(otherGene))
                    return false;
            }
            return true;
        }
        public override bool Equals(object obj) => Equals(obj as GenomeData);
        public override int GetHashCode()
        {
            if (GenomeData.Genes == null) return 0;
            int hash = 42;
            foreach (var gene in GenomeData.Genes)
                hash = hash * 37 + (gene != null ? gene.GetHashCode() : 0);
            return hash;
        }
        public static bool operator ==(Genome a, Genome b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }
        public static bool operator !=(Genome a, Genome b)
        {
            return !(a == b);
        }
        #endregion

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

            hybridData.MushName = NameGenerator.Instance?.GenerateRandomName() ?? string.Empty;

            return new Genome { GenomeData = hybridData };
        }

        public void ExpressOn(Mushroom mushroom)
        {
            if (GenomeData == null)
                return;

            // always express variant first!
            var variantGene = GenomeData.Genes.OfType<VariantGene>().First();
            variantGene.ExpressOn(mushroom);

            foreach (IGene gene in GenomeData.Genes.Where(g => g != variantGene))
            {
                gene.ExpressOn(mushroom);
            }
        }
    }
}
