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

        private void Start()
        {
            Background.SetActive(true);
            _tooltipText = GetComponentInChildren<TextMeshPro>();
            Background.SetActive(false);
        }
        private void OnEnable()
        {
            EventManager.StartListening<ITip>(EventManager.Events.OnMouseOver, ActivateTooltip);
            EventManager.StartListening(EventManager.Events.OnMouseExit, DeactivateTooltip);
        }
        private void OnDisable()
        {

            EventManager.StopListening<ITip>(EventManager.Events.OnMouseOver, ActivateTooltip);
            EventManager.StopListening(EventManager.Events.OnMouseExit, DeactivateTooltip);
        }

        void ActivateTooltip(ITip tip)
        {
            Background.SetActive(true);
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            this.transform.position = mouseWorldPos;

            if (tip.type == TipType.Shroom)
            {

                _tooltipText.text = ((MushroomTip)tip).ShroomName;//TODO: Add remaining lifespan to tooltip
                                                                  //+ "\nDecay in : " + ((MushroomTip)tip).LifeLeft.ToString(".0");

            }

            if (tip.type == TipType.Spore)
            {

            }

            if (tip.type == TipType.Item)
            {

            }
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
