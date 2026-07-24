using UnityEngine;

namespace FarmJam2026
{
    [RequireComponent(typeof(Animator))]
    public class DayNightAniimatorHandler : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null )
            {
                Debug.LogError($"Missing Animator on game object {gameObject.name} wich require one");
            }
        }
        private void OnEnable()
        {
            EventManager.StartListening(EventManager.Events.OnStartDay, NightToDay);
            EventManager.StartListening(EventManager.Events.OnStartNight, DayToNight);
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.OnStartDay, NightToDay);
            EventManager.StopListening(EventManager.Events.OnStartNight, DayToNight);
        }
       
        private void DayToNight()
        {
            _animator.SetTrigger("DayToNight");
        }
        private void NightToDay()
        {
            _animator.SetTrigger("NightToDay");
        }
    }
}
