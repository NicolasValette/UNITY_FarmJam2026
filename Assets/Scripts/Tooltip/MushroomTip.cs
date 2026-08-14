using System.Linq;
using FarmJam2026.Assets.Scripts.Genetics.Genes;
using UnityEngine;

namespace FarmJam2026.Assets.Scripts.Tooltip
{
    public class MushroomTip : ITip
    {
        public float LifeLeft;
        public string ShroomName;
        public GenomeData GenomeData;
        public Mushroom Mush;
        public TipType type { get => TipType.Shroom; }

        public Vector2 Position { get; set; }

        public string GetMessage()
        {
            ColorGene colorGene = GenomeData.Genes.OfType<ColorGene>().FirstOrDefault();
            VariantGene varGene = GenomeData.Genes.OfType<VariantGene>().FirstOrDefault();
            return TipMessageMaker.MushroomMessage(ShroomName, colorGene, varGene, Mush.CanHarvest);
        }
    }
}
