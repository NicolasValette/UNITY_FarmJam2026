using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    [Serializable]
    public class ColorGene : IGene
    {
        [SerializeField]
        public Color Color;

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.MushroomColor = Color;

            SpriteRenderer rend = mushroom.GetComponent<SpriteRenderer>();
            if (rend == null)
            {
                Debug.LogError("Missing Sprite Renderer on mushroom", mushroom.gameObject);
                return;
            }
            rend.color = Color;
        }

       
        public void PerformHybridization(List<Genome> genomes)
        {
            
            List<Color> colors = new List<Color>();
            foreach (Genome genome in genomes)
            {
                foreach (ColorGene colorGene in genome.Genes)
                {
                    colors.Add(colorGene.Color);
                }
            }

            if (colors.Count == 0) return;
            Color mixedColor = colors[0];
            for (int i = 1; i < colors.Count; i++)
            {
                mixedColor += colors[i];
            }
            mixedColor /= colors.Count;

            Color = mixedColor;
        }
    }
}
