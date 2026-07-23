using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public class Player : MonoBehaviour
    {
      

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
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
                    Debug.Log("Harvested " + spores.Count + " spores.");
                }

                IField field = hit.collider.GetComponent<IField>();
                if (field != null)
                {
                    Debug.Log("Planting crop");
                    field.PlantCrop();
                }
            }
        }
    }
}
