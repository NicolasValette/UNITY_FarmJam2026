using System.Collections;
using UnityEngine;

namespace FarmJam2026
{
    public class Mushroom : MonoBehaviour
    {
        [SerializeField]
        private int _growthTime = 5;
        [SerializeField]
        private float _scale = 2.0f;
        [SerializeField]
        private Color _adultColor;

        #region Genome

        [SerializeField] public Genome Genome = new Genome();

        #endregion

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(Grow(_scale, _growthTime));
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator Grow(float scale, float duration)
        {
            float time = 0;
            Vector2 startingScale = transform.localScale;
            Vector2 targetScale = transform.localScale * scale;

            while (time < duration)
            {
                transform.localScale = Vector2.Lerp(startingScale, targetScale, time / duration);
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
    }
}
