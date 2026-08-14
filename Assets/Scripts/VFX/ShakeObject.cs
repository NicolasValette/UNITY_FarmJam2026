using System.Collections;
using UnityEngine;

namespace FarmJam2026
{
    public class ShakeObject : MonoBehaviour
    {

        [Tooltip("Duration of the shake")]
        [SerializeField]
        private float _shakeDuration;
        [Tooltip("The strench of the shake, higher mean more camera movement")]
        [SerializeField]
        private float _shakeStrength;
        [Tooltip("Smoothness of the shake, closer to 0 mean smoother movement")]
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _shakeSmoothness;

        private Vector3 _originalPosition;

        public float ShakeStrengh { get => _shakeStrength; set => _shakeStrength = value; }
        public float ShakeSmoothness { get => _shakeSmoothness; set => _shakeSmoothness = value; }
        public float ShakeDuration
        {
            get => _shakeDuration;
            set
            {
                if (value < 0f)
                {
                    value = 0f;
                }
                _shakeDuration = value;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            _originalPosition = transform.localPosition;
        }


        public void Shake()
        {
            StartCoroutine(RunShake());
        }

        private IEnumerator RunShake()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _shakeDuration)
            {
                // I choose randomly one point, then we move the camera in this direction. I use linear interpolation to move smoothly the camera and don't produce an hard shaking.
                Vector2 randomPosition = Random.insideUnitCircle;
                transform.localPosition = Vector3.Lerp(_originalPosition, _originalPosition + new Vector3(randomPosition.x, randomPosition.y, 0f), _shakeSmoothness) * _shakeStrength;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = _originalPosition;
        }
    }
}
