using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Represents a mushroom in the game. It grows over time, produces spores, can be harvested and start decay after its lifetime expires.
    /// </summary>
    public class Mushroom : MonoBehaviour, IHarvestable
    {
        #region Serialized Fields
        [SerializeField]
        private List<GameObject> _possiblesSporesPrefabs = new List<GameObject>();
        [SerializeField]
        private List<Transform> _sporeSlots = new List<Transform>();
        #endregion

        #region Genome
        private Queue<Spore> _currentSpores = new Queue<Spore>();
        private float _currentLifeTime = 0f;
        [field: SerializeField]
        public Color Color { get; private set; }

        [SerializeField]
        public Genome Genome;

        #endregion

        #region Gene Expression
        [MushroomGeneExpression] public float GrowthTime { get; set; }
        [MushroomGeneExpression] public float LifeSpan { get; set; }
        [MushroomGeneExpression] public float SporeGrowthTime { get; set; }
        [MushroomGeneExpression] public int SporeCount { get; set; }
        [MushroomGeneExpression] public float Scale { get; set; }
        [MushroomGeneExpression] public Color MushroomColor { get; set; }
        #endregion

        private void OnValidate()
        {
            ExpressGenome();
        }

        void Start()
        {
            ExpressGenome();
            StartCoroutine(Grow(Scale, GrowthTime));
            _currentLifeTime = 0f;
        }

        void Update()
        {
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime >= LifeSpan)
            {
                Decay();
            }
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
            
            for (int i = 0; i< SporeCount; i++)
            {  
                StartGrowSpore();
            }
        }

        private void StartGrowSpore()
        {
            GameObject sporePrefab = GameObject.Instantiate(_possiblesSporesPrefabs[UnityEngine.Random.Range(0, _possiblesSporesPrefabs.Count)],
                                                            _sporeSlots[_currentSpores.Count].position, Quaternion.identity, _sporeSlots[_currentSpores.Count]);
            Spore spore = sporePrefab.GetComponent<Spore>();
            spore.InitSpore(SporeGrowthTime);
            _currentSpores.Enqueue(spore);
        }

        public void ExpressGenome()
        {
            if (Genome == null)
                return;

            foreach (IGene gene in Genome.Genes)
            {
                gene.ExpressOn(this);
            }
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
            for (int i = 0; i < SporeCount; i++)
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
