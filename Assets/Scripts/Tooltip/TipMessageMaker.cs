using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using FarmJam2026.Assets.Scripts.Genetics.Genes;
using UnityEngine;

namespace FarmJam2026.Assets.Scripts.Tooltip
{
    public static class TipMessageMaker
    {
        private static string ColorHtml_Green;
        private static string ColorHtml_Red;

        static TipMessageMaker()
        {
            ColorHtml_Green = ColorUtility.ToHtmlStringRGB(Color.darkGreen);
            ColorHtml_Red = ColorUtility.ToHtmlStringRGB(Color.red);
        }

        public static string MushroomMessage(string name, ColorGene colorGene, VariantGene varGene, bool canHarvest)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<i>{name}</i>");

            sb.AppendLine(ColorAndVariantMessage(colorGene, varGene));

            sb.AppendLine(canHarvest
                ? $"<color=#{ColorHtml_Green}>Can be harvested</color>"
                : $"<color=#{ColorHtml_Red}>Cannot be harvested");

            return sb.ToString();
        }

        public static string SporeMessage(string name, GrowthGene growthGene, SporeProductionGene prodGene, VariantGene varGene,
            bool isPlanted, ColorGene colorGene)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<i>{name}</i>");

            sb.AppendLine($"Maturity: {growthGene.GrowthTime.ToString(".0")} - Decay: {growthGene.LifeSpan.ToString(".0")}");

            var efficiency = ((float)prodGene.SporeCount) * 60.0f / prodGene.SporeGrowthTime;
            var efficiencyStr = efficiency <= 0.0f ? "Sterile" : efficiency.ToString(".0");
            sb.AppendLine($"Efficiency: {efficiencyStr} spore / min");

            if (isPlanted)
            {
                sb.AppendLine(ColorAndVariantMessage(colorGene, varGene));
            }

            return sb.ToString();
        }

        private static string ColorAndVariantMessage(ColorGene colorGene, VariantGene varGene)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Variant = {varGene.PrimaryVariation.ToString()[0]}{varGene.SecondaryVariation.ToString()[0]}");

            var colorHtml = ColorUtility.ToHtmlStringRGB(colorGene.Color);
            sb.AppendLine($"<color=#{colorHtml}>●</color> = {colorGene.Color1.ToString()[0]}{colorGene.Color2.ToString()[0]}{colorGene.Shade.ToString()[0]}");

            return sb.ToString();
        }
    }
}