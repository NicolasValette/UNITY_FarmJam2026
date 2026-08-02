using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Class to store the best value for each gene of one specific body type Mushroom
    /// </summary>
    [Serializable]
    public class BodyTypeData
    {
        public BodyType Type { get; private set; }
        #region Biomass Gene
        public int MaximumBiomass { get; private set; }
        public int MinimumBiomass { get; private set; }
        #endregion
        #region Color Gene
        public List<Color> Colors { get; private set; }
        #endregion
        #region Growth Time Gene
        public float MaximumGrowthTime { get; private set; }
        public float MinimumGrowthTime { get; private set; }
        public float MaximumLifeSpan { get; private set; }
        public float MinimumLifeSpan { get; private set; }
        #endregion
        #region Size Gene
        public float MaximumHorizontalScale { get; private set; }
        public float MinimumHorizontalScale { get; private set; }
        public float MaximumVerticalScale { get; private set; }
        public float MinimumVerticalScale { get; private set; }
        #endregion
        #region Spore Prodution Gene
        public int MaximumSporeProduction { get; private set; }
        public int MinimumSporeProduction { get; private set; }
        public float MaximumSporeGrowthTime { get; private set; }
        public float MinimumSporeGrowthTime { get; private set; }
        #endregion

        public BodyTypeData (GenomeData genome)
        {
            foreach (var gene in genome.Genes)
            {
                if (gene is BiomassGene biomassGene)
                {
                    MaximumBiomass = biomassGene.BiomassValue;
                    MinimumBiomass = biomassGene.BiomassValue;
                }
                else if (gene is ColorGene colorGene)
                {
                    Colors = new List<Color>
                    {
                        colorGene.Color
                    };
                }
                else if (gene is GrowthGene growthGene)
                {
                    MaximumGrowthTime = growthGene.GrowthTime;
                    MinimumGrowthTime = growthGene.GrowthTime;
                    MaximumLifeSpan = growthGene.LifeSpan;
                    MinimumLifeSpan = growthGene.LifeSpan;
                }
                else if (gene is SizeGene sizeGene)
                {
                    MaximumHorizontalScale = sizeGene.HorizontalScale;
                    MinimumHorizontalScale = sizeGene.HorizontalScale;
                    MaximumVerticalScale = sizeGene.VerticalScale;
                    MinimumVerticalScale = sizeGene.VerticalScale;
                }
                else if (gene is SporeProductionGene sporeProdGene)
                {
                    MaximumSporeProduction = sporeProdGene.SporeCount;
                    MinimumSporeProduction = sporeProdGene.SporeCount;
                    MaximumSporeGrowthTime = sporeProdGene.SporeGrowthTime;
                    MinimumSporeGrowthTime = sporeProdGene.SporeGrowthTime;
                }
                else if (gene is BodyTypeGene bodyTypeGene)
                {
                    Type = bodyTypeGene.BodyType;
                }
                else
                {
                    Debug.LogError($"Gene of type {gene.GetType()} not supported in Gene Book");
                }
            }
        }
        public void UpdateValue (GenomeData genome)
        {
            foreach (var gene in genome.Genes)
            {
                if (gene is BiomassGene biomassGene)
                {
                    MaximumBiomass = Mathf.Max(biomassGene.BiomassValue, MaximumBiomass);
                    MinimumBiomass = Mathf.Min(biomassGene.BiomassValue, MinimumBiomass);
                }
                else if (gene is ColorGene colorGene)
                {
                    Colors.Add(colorGene.Color);
                }
                else if (gene is GrowthGene growthGene)
                {
                    MaximumGrowthTime = Mathf.Max(growthGene.GrowthTime,MaximumGrowthTime);
                    MinimumGrowthTime = Mathf.Min(growthGene.GrowthTime, MinimumGrowthTime);
                    MaximumLifeSpan = Mathf.Max(growthGene.LifeSpan,MaximumLifeSpan);
                    MinimumLifeSpan = Mathf.Min(growthGene.LifeSpan, MinimumLifeSpan);
                }
                else if (gene is SizeGene sizeGene)
                {
                    MaximumHorizontalScale = Mathf.Max(sizeGene.HorizontalScale, MaximumHorizontalScale);
                    MinimumHorizontalScale = Mathf.Min(sizeGene.HorizontalScale, MinimumHorizontalScale);
                    MaximumVerticalScale = Mathf.Max(sizeGene.VerticalScale, MaximumVerticalScale);
                    MinimumVerticalScale = Mathf.Min(sizeGene.VerticalScale, MinimumVerticalScale);
                }
                else if (gene is SporeProductionGene sporeProdGene)
                {
                    MaximumSporeProduction = Mathf.Max(sporeProdGene.SporeCount, MaximumSporeProduction);
                    MinimumSporeProduction = Mathf.Min(sporeProdGene.SporeCount, MinimumSporeProduction);
                    MaximumSporeGrowthTime = Mathf.Max(sporeProdGene.SporeGrowthTime, MaximumSporeGrowthTime);
                    MinimumSporeGrowthTime = Mathf.Min(sporeProdGene.SporeGrowthTime, MinimumSporeGrowthTime);
                }
                else if (gene is BodyTypeGene bodyTypeGene)
                {
                    //Do Nothing
                }
                else
                {
                    Debug.LogError($"Gene of type {gene.GetType()} not supported in Gene Book");
                }
            }
        }
        
    }
}
