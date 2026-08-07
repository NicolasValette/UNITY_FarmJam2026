using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FarmJam2026
{
    public class LightManager : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField]
        private Light2D _globalLight;
        [Space]
        [Header("Lighting options")]
        [SerializeField]
        private float _dayIntensity;
        [SerializeField]
        private Color _dayColor;
        [SerializeField]
        private float _nightIntensity; 
        [SerializeField]
        private Color _nightColor;
        [Space]
        [SerializeField]
        private float _switchDuration = 5f;

        
        private void OnEnable()
        {
            EventManager.StartListening(EventManager.Events.OnStartDay, () => ChangeLightingSetting(true));
            EventManager.StartListening(EventManager.Events.OnStartNight, () => ChangeLightingSetting(false));
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.OnStartDay, () => ChangeLightingSetting(true));
            EventManager.StopListening(EventManager.Events.OnStartNight, () => ChangeLightingSetting(false));
           
           
        }

        public void ChangeLightingSetting(bool _isDayTime)
        {
            if (_isDayTime)
            {
                //_globalLight.intensity = _dayIntensity;
                //_globalLight.color = _dayColor;
                StartCoroutine(Switch(_nightColor, _dayColor, _nightIntensity, _dayIntensity));
            }
            else
            {
                //_globalLight.intensity = _nightIntensity;
                //_globalLight.color = _nightColor;
                StartCoroutine(Switch(_dayColor, _nightColor, _dayIntensity, _nightIntensity));
            }
        }
        private IEnumerator Switch(Color startingColor, Color targetColor, float startingIntensity, float targetIntensity)
        {
            float time = 0;
            while (time < _switchDuration)
            {
                _globalLight.color = Color.Lerp(startingColor, targetColor, time / _switchDuration);
                _globalLight.intensity = Mathf.Lerp(startingIntensity, targetIntensity, time / _switchDuration);
                time += Time.deltaTime;
                yield return null;
            }
            _globalLight.color = targetColor;
            _globalLight.intensity = targetIntensity;
        }

    }
}
