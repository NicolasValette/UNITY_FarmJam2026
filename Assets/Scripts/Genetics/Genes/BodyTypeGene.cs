using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    /// <summary>
    /// Type of body of this mushroom
    /// </summary>
    [Serializable]
    public class BodyTypeGene : IGene
    {
        [SerializeField]
        public Sprite BodyTypeSprite;
        [SerializeField]
        public BodyType BodyType;
        public bool Equals(IGene other)
        {
            var spriteOther = other as BodyTypeGene;
            if (spriteOther == null)
                return false;

            return EqualityComparer<Sprite>.Default.Equals(BodyTypeSprite, spriteOther.BodyTypeSprite);
        }

        public void ExpressOn(Mushroom mushroom)
        {
            mushroom.MushroomBodyType = BodyTypeSprite;

            SpriteRenderer rend = mushroom.GetComponent<SpriteRenderer>();
            if (rend == null)
            {
                Debug.LogError("Missing Sprite Renderer on mushroom", mushroom.gameObject);
                return;
            }
            rend.sprite = BodyTypeSprite;
        }

        public void PerformHybridization(List<Genome> genomes)
        {
            var bodyGenes = genomes.SelectMany(g => g.GenomeData.Genes).OfType<BodyTypeGene>().ToList();

            var randomPick = bodyGenes[Random.Range(0, bodyGenes.Count)];
            BodyType = randomPick.BodyType;
            BodyTypeSprite = randomPick.BodyTypeSprite;

            //TODO in order to make random mutation happen, we need a data dict matching BodyType and Sprite!!
        }
    }
}
