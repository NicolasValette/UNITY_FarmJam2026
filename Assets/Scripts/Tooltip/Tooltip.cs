using FarmJam2026.Assets.Scripts.Tooltip;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public class Tooltip : MonoBehaviour
    {
        TextMeshPro _tooltipText;
        public GameObject Background;
        [SerializeField]
        private Vector2 _sporeOffset;
        [SerializeField]
        private Vector2 _mushroomOffset;

        private ITip _currentTip;
        private bool IsActivated = false;
        private void Start()
        {
            Background.SetActive(true);
            _tooltipText = GetComponentInChildren<TextMeshPro>();
            Background.SetActive(false);
        }
        private void OnEnable()
        {
            EventManager.StartListening<ITip>(EventManager.Events.OnMouseEnter, ActivateTooltip);
            EventManager.StartListening(EventManager.Events.OnMouseExit, DeactivateTooltip);
        }
        private void OnDisable()
        {

            EventManager.StopListening<ITip>(EventManager.Events.OnMouseEnter, ActivateTooltip);
            EventManager.StopListening(EventManager.Events.OnMouseExit, DeactivateTooltip);
        }
        void Update()
        {
            if (IsActivated)
            {
                _tooltipText.text = _currentTip.GetMessage();
            }
        }

        void ActivateTooltip(ITip tip)
        {
            _currentTip = tip;
            IsActivated = true;
            Background.SetActive(true);
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

            if (tip.type == TipType.Shroom)
            {
                Background.transform.position = tip.Position + _mushroomOffset;
            }
            else if (tip.type == TipType.Spore)
            {
                Background.transform.position = tip.Position + _sporeOffset; ;
            }

            

            //if (tip.type == TipType.Item)
            //{

            //}
        }

        void UpdatePosition()
        {
           
        }

        void DeactivateTooltip()
        {
            _currentTip = null;
            IsActivated = false;
            Background.SetActive(false);
        }
    }
}
