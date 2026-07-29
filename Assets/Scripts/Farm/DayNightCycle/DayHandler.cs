using UnityEngine;

namespace FarmJam2026
{
    public class DayHandler : MonoBehaviour
    {
        
        [SerializeField, Tooltip("Duration of the day in seconds")]
        private float _dayDuration = 10f;
        [SerializeField, Tooltip("Duration of the night in seconds")]
        private float _nightDuration = 5f;

        private bool _isDayTime = true;
        private float _currentTime = 0f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _currentTime = 0f;
            //The game start on Day time
            _isDayTime = true;
        }

        // Update is called once per frame
        void Update()
        {
            _currentTime += Time.deltaTime;
            if ((_currentTime > 0 && _currentTime < _dayDuration && !_isDayTime) ||
                (_currentTime > 0 && _currentTime > _dayDuration && _currentTime < (_dayDuration + _nightDuration) && _isDayTime))
            {
                ChangeTime();
            }
            else if (_currentTime > _dayDuration + _nightDuration)
            {
                _currentTime = 0f;
                ChangeTime();
            }
        }
        private void ChangeTime()
        {
            if (_isDayTime)
            {
                Debug.Log($"Change time frome Day to Night");
                EventManager.TriggerEvent(EventManager.Events.OnStartNight);
            }
            else
            {
                Debug.Log($"Change time frome Night to Day");
                EventManager.TriggerEvent(EventManager.Events.OnStartDay);
            }
            
            _isDayTime = !_isDayTime;
        }
    }
}
