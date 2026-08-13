using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FarmJam2026.Assets.Scripts.Tooltip
{
    public class MushroomTip : ITip
    {
        public float LifeLeft;
        public string ShroomName;
        public GenomeData GenomeData;
        public TipType type { get => TipType.Shroom; }

        public Vector2 Position { get; set; }

        public string GetMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ShroomName);
            ColorGene colorGene = GenomeData.Genes.OfType<ColorGene>().FirstOrDefault();
            var color = ColorUtility.ToHtmlStringRGB(colorGene.Color);

            sb.AppendLine($"<color=#{color}>●</color> = {colorGene.Color1.ToString()[0]}{colorGene.Color2.ToString()[0]}{colorGene.Shade.ToString()[0]}");
            return sb.ToString();
        }
    }
}
