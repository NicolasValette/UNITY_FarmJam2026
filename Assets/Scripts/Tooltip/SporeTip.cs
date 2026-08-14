using System.Linq;
using FarmJam2026.Assets.Scripts.Genetics.Genes;
using FarmJam2026.Assets.Scripts.Tooltip;
using UnityEngine;

namespace FarmJam2026
{
    public class SporeTip : ITip
    {
        public float GrowthTime;
        public float LifeTime;
        public int SporeNumber;
        public int BiomassQuantity;
        public TipType type { get => TipType.Spore; }
        public Vector2 Position { get; set; }
        public bool IsPlanted;
        public GenomeData GenomeData;

        public string GetMessage()
        {
            ColorGene colorGene = GenomeData.Genes.OfType<ColorGene>().FirstOrDefault();
            GrowthGene growthGene = GenomeData.Genes.OfType<GrowthGene>().FirstOrDefault();
            SporeProductionGene prodGene = GenomeData.Genes.OfType<SporeProductionGene>().FirstOrDefault();
            VariantGene varGene = GenomeData.Genes.OfType<VariantGene>().FirstOrDefault();
            return TipMessageMaker.SporeMessage(GenomeData.MushName, growthGene, prodGene, varGene,
                IsPlanted, colorGene);
        }
    }
}
