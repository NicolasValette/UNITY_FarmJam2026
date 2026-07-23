using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Represents a mushroom in the game. It grows over time, produces spores, can be harvested and start decay after its lifetime expires.
    /// </summary>
    public class Mushroom : MonoBehaviour, IHarvestable
    {
        #region Serialized Fields
        [Header("Mushroom Growth Settings")]
        [SerializeField]
        private float _lifeTime = 15f;
        [SerializeField]
        private float _growthTime = 5f;
        [SerializeField]
        private float _sporeGrowthTime = 5f;
        [SerializeField]
        private int _harvestValue = 2;
        [SerializeField]
        private List<GameObject> _possiblesSporesPrefabs = new List<GameObject>();
        [SerializeField]
        private List<Transform> _sporeSlots = new List<Transform>();
        [SerializeField]
        private float _scale = 2.0f;
        [SerializeField]
        private Color _adultColor;
        #endregion

        private Genome _genome;
        private Queue<Spore> _currentSpores = new Queue<Spore>();
        private float _currentLifeTime = 0f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(Grow(_scale, _growthTime));
            _currentLifeTime = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            _currentLifeTime += Time.deltaTime;
            
            if (_currentLifeTime >= _lifeTime)
            {
                Decay();
            }
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
            for (int i = 0; i< _harvestValue; i++)
            {  
                StartGrowSpore();
            }
        }

        private void StartGrowSpore()
        {
            GameObject sporePrefab = GameObject.Instantiate(_possiblesSporesPrefabs[UnityEngine.Random.Range(0, _possiblesSporesPrefabs.Count)],
                                                            _sporeSlots[_currentSpores.Count].position, Quaternion.identity, _sporeSlots[_currentSpores.Count]);
            Spore spore = sporePrefab.GetComponent<Spore>();
            spore.InitSpore(_sporeGrowthTime);
            _currentSpores.Enqueue(spore);
        }
        public List<Spore> Harvest()
        {
            List<Spore> harvestedSpores = new List<Spore>();
            while (_currentSpores.Count > 0 && _currentSpores.Peek().HasGrown)
            {
                Spore spore = _currentSpores.Dequeue();
                Destroy(spore.gameObject);
                harvestedSpores.Add(spore);
            }
            for (int i = 0; i < _harvestValue; i++)
            {
                StartGrowSpore();
            }
            return harvestedSpores;
        }
        private void Decay()
        {
            while (_currentSpores.Count > 0)
            {
                Spore spore = _currentSpores.Dequeue();
                Destroy(spore.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
