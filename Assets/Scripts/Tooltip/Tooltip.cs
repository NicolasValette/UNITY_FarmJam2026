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

        void ActivateTooltip(ITip tip)
        {
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

            _tooltipText.text = tip.GetMessage();

            //if (tip.type == TipType.Item)
            //{

            //}
        }

        void UpdatePosition()
        {
           
        }

        void DeactivateTooltip()
        {
            Background.SetActive(false);
        }
    }
}
