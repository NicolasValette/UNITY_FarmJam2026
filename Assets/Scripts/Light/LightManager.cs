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
                _globalLight.intensity = _dayIntensity;
                _globalLight.color = _dayColor;
            }
            else
            {
                _globalLight.intensity = _nightIntensity;
                _globalLight.color = _nightColor;
            }
        }
    }
}
