using System.Collections;
using UnityEngine;

namespace FarmJam2026
{
    public class Mushroom : MonoBehaviour
    {
        [SerializeField]
        private float _scale = 2.0f;
        [SerializeField]
        private Color _adultColor;

        #region Genome

        [SerializeField, HideInInspector]
        public Genome Genome = new Genome();

        #endregion

        #region Gene Expression
        public float GrowthTime { get; set; }
        #endregion

        private void OnValidate()
        {
            ExpressGenome();
        }

        void Start()
        {
            StartCoroutine(Grow(_scale, GrowthTime));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="growthDuration">Time it takes the mushroom to reach Mature state (in seconds)</param>
        /// <returns></returns>
        private IEnumerator Grow(float scale, float growthDuration)
        {
            float time = 0;
            Vector2 startingScale = transform.localScale;
            Vector2 targetScale = transform.localScale * scale;

            while (time < growthDuration)
            {
                transform.localScale = Vector2.Lerp(startingScale, targetScale, time / growthDuration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.localScale = targetScale;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = _adultColor;
            }

        }

        public void ExpressGenome()
        {
            foreach (IGene gene in Genome.Genes)
            {
                gene.ExpressOn(this);
            }
        }
    }
}
