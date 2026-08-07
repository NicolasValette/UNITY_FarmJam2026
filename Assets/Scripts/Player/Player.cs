using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public class Player : MonoBehaviour
    {
        public SporeItem SelectedSpore { get; private set; }

        private bool _isMenuOpen = false;

        private void OnEnable()
        {
            EventManager.StartListening(EventManager.Events.OnUIMenuOpen, () =>_isMenuOpen = true);
            EventManager.StartListening(EventManager.Events.OnUIMenuClose, () => _isMenuOpen = false);
        }
        private void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.OnUIMenuOpen, () => _isMenuOpen = true);
            EventManager.StopListening(EventManager.Events.OnUIMenuClose, () => _isMenuOpen = false);
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (DragAndDropHolderFSM.Instance.CurrentState is IdleState && !_isMenuOpen && Mouse.current.leftButton.wasReleasedThisFrame)
            {
                MakeAction();
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                DragAndDropHolderFSM.Instance.HasReleased = true;
            }
        }

        private void MakeAction()
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
               
                //DragElement element = hit.collider.GetComponent<DragElement>();
                //if (element != null)
                //{
                //    DragAndDropHolderFSM.Instance.RegisteredDraggedElement(element.gameObject);
                //    return;
                //}
                IHarvestable harvestable = hit.collider.GetComponent<IHarvestable>();
                if (harvestable != null)
                {
                    List<Spore> spores = harvestable.Harvest();
                    if (spores!= null && spores.Count > 0)
                        EventManager.TriggerEvent<List<Spore>>(EventManager.Events.OnHarvest, spores);
                    Debug.Log("Harvested " + spores.Count + " spores.");

                    return;
                }

                //IField field = hit.collider.GetComponent<IField>();
                //if (field != null)
                //{
                //    Debug.Log("Planting crop");
                //    if (SelectedSpore != null && SelectedSpore.Quantity > 0)
                //    {
                //        field.PlantCrop(SelectedSpore.Spore.Genome);
                //    }
                //    else
                //        Debug.Log("No spore to plant");

                //    return;
                //}

                //IItem item = hit.collider.GetComponent<IItem>();
                //if (item != null)
                //{
                //    Debug.Log("Selecting item in Inventory");
                //    if(item.Type == ItemType.Spore)
                //    {
                //        SelectedSpore = item as SporeItem;
                //        EventManager.TriggerEvent(EventManager.Events.OnSporeSelection);
                //        SelectedSpore.Select();
                //    }

                //    return;
                //}

                IBlenderButton blenderButton = hit.collider.GetComponent<IBlenderButton>();
                if (blenderButton != null)
                {
                    blenderButton.PressTheButton(this);
                }
            }
        }
    }
}

