using FarmJam2026.Assets.Scripts.Tooltip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    /// <summary>
    /// Represents a mushroom in the game. It grows over time, produces spores, can be harvested and start decay after its lifetime expires.
    /// </summary>
    public class Mushroom : MonoBehaviour, IHarvestable, ICollectScience
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
        [MushroomGeneExpression] public float HorizontalScale { get; set; }
        [MushroomGeneExpression] public float VerticalScale { get; set; }
        [MushroomGeneExpression] public Color MushroomColor { get; set; }
        [MushroomGeneExpression] public Sprite MushroomBodyType { get; set; }
        [MushroomGeneExpression] public int BiomassValue { get; set;  }
        #endregion

        private bool _isAdult = false;

        private GameObject _variantInstance = null;

        private void OnValidate()
        {
            Genome?.ExpressOn(this);
        }

        void Start()
        {
            Genome?.ExpressOn(this);
            StartCoroutine(Grow(HorizontalScale, VerticalScale, GrowthTime));
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
        private IEnumerator Grow(float horizontalScale, float verticalScale, float growthDuration)
        {
            float time = 0;
            Vector2 startingScale = Vector2.zero;
            Vector2 targetScale = new Vector2(horizontalScale, verticalScale);
            

            while (time < growthDuration)
            {
                transform.localScale = Vector2.Lerp(startingScale, targetScale, time / growthDuration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.localScale = targetScale;
            _isAdult = true;
            EventManager.TriggerEvent<GenomeData>(EventManager.Events.OnMushroomAdult, Genome.GenomeData);
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

        public List<Spore> Harvest()
        {
            List<Spore> harvestedSpores = new List<Spore>();
            int nbOfSporeHarvested = 0;
            while (_currentSpores.Count > 0 && _currentSpores.Peek().HasGrown)
            {
                Spore spore = _currentSpores.Dequeue();
                Destroy(spore.gameObject);
                spore.Genome = Genome;
                harvestedSpores.Add(spore);
                nbOfSporeHarvested++;
            }
            for (int i = 0; i < Mathf.Min(nbOfSporeHarvested, SporeCount); i++)
            {
                StartGrowSpore();
            }
            return harvestedSpores;
        }

        private void Decay()
        {
            transform.parent.gameObject.GetComponent<Field>()?.SetFieldEmpty();
            EventManager.TriggerEvent<int>(EventManager.Events.OnMushroomDecay, BiomassValue);
            DestroyGameObject();
        }

        public void DestroyGameObject()
        {
            while (_currentSpores.Count > 0)
            {
                Spore spore = _currentSpores.Dequeue();
                Destroy(spore.gameObject);
            }
            EventManager.TriggerEvent(EventManager.Events.OnMouseExit);
            Destroy(gameObject);
        }

        public GenomeData CollectScience()
        {
            if (_isAdult)
            {
                GenomeData dataToReturn = Genome.GenomeData;
                DestroyGameObject();
                return dataToReturn;
            }
            return null;
        }
   
        private void OnMouseEnter()
        {
            Debug.Log("Mouse over Shroom");
            var Tip = new MushroomTip()
            {
                ShroomName = Genome.GenomeData.MushName,
                LifeLeft = LifeSpan - _currentLifeTime
            };
            EventManager.TriggerEvent(EventManager.Events.OnMouseEnter, Tip);
        }

        private void OnMouseExit()
        {
            EventManager.TriggerEvent(EventManager.Events.OnMouseExit);
        }

        public void ApplyVariant(MushroomVariantData variantData)
        {
            if (_variantInstance != null)
                GameObject.Destroy(_variantInstance);

            _variantInstance = GameObject.Instantiate(variantData.VariantPrefab, transform);
        }
    }
}
