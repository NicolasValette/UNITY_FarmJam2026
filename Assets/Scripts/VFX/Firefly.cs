using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    public enum FireflyType
    {
        Perlin,
        Random,
        SinCos
    }
    public class Firefly : MonoBehaviour
    {
        [SerializeField]
        private FireflyType _type;
        [Header("Perlin")]
        [SerializeField]
        private float _noiseScale = 1.0f;   // Speed of the noise change
        [SerializeField]
        private float _noiseAmount = 0.5f;  // Maximum distance offset
        [Header("Random")]
        [SerializeField]
        private float _radius = 2f;
        [SerializeField]
        private float _speed = 1.5f;

        [Header("SinCos")]
        [SerializeField]
        private float _frequency = 2f;
        [SerializeField]
        private float _amplitude = 0.5f;
      

        public FirefliesHolder FireffliesHold { get; set; }
        private float offsetA;
        private float offsetB;

       
        
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private float seedX;
        private float seedY;

        private Vector3 _initialScale;

        private void OnEnable()
        {
            EventManager.StartListening(EventManager.Events.OnStartDay, Sleep);
            EventManager.StartListening(EventManager.Events.OnStartNight, WakeUp);
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.OnStartDay, Sleep);
            EventManager.StopListening(EventManager.Events.OnStartNight, WakeUp);
        }

        void Start()
        {
            _initialScale = transform.localScale;
            Init();
            transform.localScale = Vector2.zero;
        }
        private void Update()
        {
            switch(_type)
            {
                case FireflyType.Perlin:
                    UpdatePerlin();
                    break;
                case FireflyType.Random:
                    UpdateRandom();
                    break;
                case FireflyType.SinCos:
                    UpdateSinCos();
                    break;
            }
        }
        private void Init()
        {
            var v = Random.insideUnitCircle * FireffliesHold.Radius;
            transform.localPosition = new Vector3(v.x, v.y, transform.localPosition.z);
            startPosition = transform.position;
            GetNewTarget();
            seedX = Random.Range(0f, 1000f);
            seedY = Random.Range(0f, 1000f);
            offsetA = Random.Range(0f, 100f);
            offsetB = Random.Range(0f, 100f);
        }
        void UpdatePerlin()
        {
            float noiseX = Mathf.PerlinNoise(seedX + Time.time * _noiseScale, 0.0f) * 2.0f - 1.0f;
            float noiseY = Mathf.PerlinNoise(0.0f, seedY + Time.time * _noiseScale) * 2.0f - 1.0f;

            Vector3 offset = new Vector3(noiseX, noiseY, 0f) * _noiseAmount;
            transform.position = startPosition + offset;

        }
        void UpdateRandom()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                GetNewTarget();
            }
        }
        void UpdateSinCos()
        {
            float x = Mathf.Sin((Time.time + offsetA) * _frequency) * _amplitude;
            float y = Mathf.Cos((Time.time + offsetB) * (_frequency * 1.3f)) * _amplitude;

            transform.position = startPosition + new Vector3(x, y, 0f);
        }
        void GetNewTarget()
        {
            Vector2 randomCircle = Random.insideUnitCircle * _radius;
            targetPosition = startPosition + new Vector3(randomCircle.x, randomCircle.y, 0f);
        }

        private void WakeUp()
        {
            Init();
            StartCoroutine(Fade(Vector2.zero, _initialScale, Random.Range(1f,6f)));
        }

        private void Sleep()
        {
            StartCoroutine(Fade(_initialScale, Vector2.zero, Random.Range(1f, 6f)));
        }
        private IEnumerator Fade(Vector2 initialScale, Vector2 targetScale, float duration)
        {
            float time = 0;
            
            while (time < duration)
            {
                transform.localScale = Vector2.Lerp(initialScale, targetScale, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            transform.localScale = targetScale;
        } 
    }
}
