using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private List<Genome> _genomePocket;

        private int _selectedGenome = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                Debug.Log("First Mush selected");
                _selectedGenome = 0;
            }
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                Debug.Log("Second Mush selected");
                _selectedGenome = 1;
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                MakeAction();
            }
        }

        private void MakeAction()
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                
                IHarvestable harvestable = hit.collider.GetComponent<IHarvestable>();
                if (harvestable != null)
                {
                    List<Spore> spores = harvestable.Harvest();
                    EventManager.m_harvest.Invoke(spores);
                    Debug.Log("Harvested " + spores.Count + " spores.");
                }

                IField field = hit.collider.GetComponent<IField>();
                if (field != null)
                {
                    Debug.Log("Planting crop");
                    field.PlantCrop(_genomePocket[_selectedGenome]);
                }
            }
        }
    }
}
