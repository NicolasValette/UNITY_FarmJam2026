using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace FarmJam2026
{
    [Serializable]
    public class Spore : MonoBehaviour
    {

        private float _growthTime = 5f;

        public bool HasGrown { get; private set; } = false;

       

        private void Update()
        {
            
        }

        public void InitSpore(float growthTime)
        {
            Debug.Log("Spore started growing");
            _growthTime = growthTime;
            StartCoroutine(Grow());
        }
        private IEnumerator Grow()
        {
            float time = 0;
            while (time < _growthTime)
            {
                transform.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, time / _growthTime);
                time += Time.deltaTime;
                yield return null;
            }
            transform.localScale = Vector2.one;
            HasGrown = true;
        }

        public void  SetGrowthTime (int growthTime)
        {
            _growthTime = growthTime;
        }



    }
}