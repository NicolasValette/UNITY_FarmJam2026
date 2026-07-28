using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

namespace FarmJam2026
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private List<GenomeData> _genomePocket;

        public SporeItem SelectedSpore { get; private set; }
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
                    EventManager.TriggerEvent<List<Spore>>(EventManager.Events.OnHarvest, spores);
                    Debug.Log("Harvested " + spores.Count + " spores.");

                    return;
                }

                IField field = hit.collider.GetComponent<IField>();
                if (field != null)
                {
                    Debug.Log("Planting crop");
                    if (SelectedSpore != null && SelectedSpore.Quantity > 0)
                    {
                        field.PlantCrop(SelectedSpore.Spore.GenomeToGrow);
                    }
                    else
                        Debug.Log("No spore to plant");

                    return;
                }

                IItem item = hit.collider.GetComponent<IItem>();
                if (item != null)
                {
                    Debug.Log("Selecting item in Inventory");
                    if(item.Type == ItemType.Spore)
                    {
                        SelectedSpore = item as SporeItem;
                    }

                    return;
                }

                IBlenderButton blenderButton = hit.collider.GetComponent<IBlenderButton>();
                if (blenderButton != null)
                {
                    blenderButton.PressTheButton(this);
                }
            }
        }
    }
}
