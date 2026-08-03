using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    /// <summary>
    /// Color of the mushroom
    /// </summary>
    [Serializable]
    public class ColorGene : IGene
    {
        [SerializeField] public EGeneColor Color1;
        [SerializeField] public EGeneColor Color2;
        [SerializeField] public EGeneShade Shade;

        public bool Equals(IGene other)
        {
            var colorOther = other as ColorGene;
            if (colorOther == null)
                return false;

            return EqualityComparer<EGeneColor>.Default.Equals(Color1, colorOther.Color1)
                && EqualityComparer<EGeneColor>.Default.Equals(Color2, colorOther.Color2)
                && EqualityComparer<EGeneShade>.Default.Equals(Shade, colorOther.Shade);
        }

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.MushroomColor = Color.black; //TODO color dico

            SpriteRenderer rend = mushroom.GetComponent<SpriteRenderer>();
            if (rend == null)
            {
                Debug.LogError("Missing Sprite Renderer on mushroom", mushroom.gameObject);
                return;
            }
            rend.color = Color.black; //TODO color dico
        }

       
        public void PerformHybridization(List<Genome> genomes)
        {
            var colorGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<ColorGene>().ToList();

            // pick colors
            var allColors = colorGenes.Select(g => g.Color1).ToList();
            allColors.AddRange(colorGenes.Select(g => g.Color2));

            var colorId = Random.Range(0, allColors.Count);
            Color1 = allColors[colorId];
            allColors.RemoveAt(colorId);
            colorId = Random.Range(0, allColors.Count);
            Color2 = allColors[colorId];

            var colorValues = Enum.GetValues(typeof(EGeneColor));
            var roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                Color1 = (EGeneColor)Random.Range(0, colorValues.Length);
            }
            roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                Color2 = (EGeneColor)Random.Range(0, colorValues.Length);
            }

            // pick shade
            Shade = colorGenes[Random.Range(0, allColors.Count)].Shade;
            roll = Random.Range(0f, 1f);
            if (roll < GameOptions.Instance.MutationChance)
            {
                var shadeValues = Enum.GetValues(typeof(EGeneShade));
                Shade = (EGeneShade)Random.Range(0, shadeValues.Length);
            }
        }

        public Color Color => Color.black; //TODO color dico

        public ColorName ColorName
        {
            get
            {
                // 0 = red, 1 = blue, 2 = yellow
                var colorIds = new List<int> { (int)Color1, (int)Color2 };
                colorIds.Sort();

                switch (Shade)
                {
                    case EGeneShade.Medium:
                        {
                            if (colorIds[0] == 0)
                            {
                                return colorIds[1] == 0 ? ColorName.Red
                                    : colorIds[1] == 1 ? ColorName.Purple
                                    : ColorName.Orange;
                            }
                            else if (colorIds[0] == 1)
                            {
                                return colorIds[1] == 1 ? ColorName.Blue
                                    : ColorName.Green;
                            }
                            else return ColorName.Yellow;
                        }
                    case EGeneShade.Dark:
                        {
                            if (colorIds[0] == 0)
                            {
                                return colorIds[1] == 0 ? ColorName.DarkRed
                                    : colorIds[1] == 1 ? ColorName.DarkPurple
                                    : ColorName.DarkOrange;
                            }
                            else if (colorIds[0] == 1)
                            {
                                return colorIds[1] == 1 ? ColorName.DarkBlue
                                    : ColorName.DarkGreen;
                            }
                            else return ColorName.DarkYellow;
                        }
                    case EGeneShade.Light:
                        {
                            if (colorIds[0] == 0)
                            {
                                return colorIds[1] == 0 ? ColorName.LightRed
                                    : colorIds[1] == 1 ? ColorName.LightPurple
                                    : ColorName.LightOrange;
                            }
                            else if (colorIds[0] == 1)
                            {
                                return colorIds[1] == 1 ? ColorName.LightBlue
                                    : ColorName.LightGreen;
                            }
                            else return ColorName.LightYellow;
                        }
                }

                throw new Exception("IMPOSSIBLE COLOR");
            }
        }
    }
}
