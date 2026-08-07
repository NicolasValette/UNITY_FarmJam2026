using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    /// <summary>
    /// Defines if an accessory is active or not.
    /// </summary>
    [Serializable]
    public abstract class AccessoryGene : IGene
    {
        [SerializeField]
        public bool IsActive = false;

        public virtual bool Equals(IGene other)
        {
            var accessoryOther = other as AccessoryGene;
            if (accessoryOther == null)
                return false;

            return EqualityComparer<bool>.Default.Equals(IsActive, accessoryOther.IsActive);
        }

        public virtual void ExpressOn(Mushroom mushroom)
        {
            Debug.Log($"This shouldn't be called.");
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var accessoryGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<AccessoryGene>().ToList();

            IsActive = accessoryGenes[Random.Range(0, accessoryGenes.Count)].IsActive;
            var roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                IsActive = Random.Range(0, 2) == 1;
            }
        }
    }
}