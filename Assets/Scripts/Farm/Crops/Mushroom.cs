using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FarmJam2026
{
    public class Mushroom : MonoBehaviour
    {
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

        private Genome _genome;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(Grow(_scale, _growthTime));
            _currentLifeTime = 0f;
            Debug.Log($"Mushroom {gameObject.name} started growing");
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
            Debug.Log($"Mushroom {gameObject.name} started growing Spore");
            GameObject sporePrefab = GameObject.Instantiate(_possiblesSporesPrefabs[UnityEngine.Random.Range(0, _possiblesSporesPrefabs.Count)],
                                                            _sporeSlots[_currentSpores.Count].position, Quaternion.identity, _sporeSlots[_currentSpores.Count]);
            Spore spore = sporePrefab.GetComponent<Spore>();
            spore.InitSpore(_sporeGrowthTime);
            _currentSpores.Enqueue(spore);
        }
        public Spore Harvest()
        {
            if (_currentSpores.Peek().HasGrown)
            {
                Spore spore = _currentSpores.Dequeue();
                Debug.Log("Spore harvested");
                Destroy(spore.gameObject);
                StartGrowSpore();
                return spore;
            }
            else
            {
                return null;
            }
        }
        private void Decay()
        {
            Debug.Log($"Mushroom {gameObject.name} decayed");
            while (_currentSpores.Count > 0)
            {
                Spore spore = _currentSpores.Dequeue();
                Destroy(spore.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
