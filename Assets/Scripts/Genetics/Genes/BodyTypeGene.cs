using System;
using System.Collections.Generic;
using UnityEngine;

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
            throw new System.NotImplementedException();
        }
    }
}
