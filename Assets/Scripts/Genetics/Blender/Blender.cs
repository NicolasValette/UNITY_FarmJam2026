using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class Blender : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private TMP_Text _contentAmount;
        public List<Genome> Content { get; private set; } = new List<Genome>();
       
 

        
        private void Update()
        {
            _contentAmount.text = Content.Count.ToString();
            
        }

        public void AddToBlender(Genome genome)
        {
            Debug.Log("Add spore to blender");
            Content.Add(genome);
            EventManager.TriggerEvent(EventManager.Events.OnAddToBlender, genome);
        }
        public void OnDrop(PointerEventData eventData)
        {
            
            if (DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.InventorySpore)
            {
                //var blenderAdd = gameObject.GetComponentInChildren<BlenderAdd>();
                //if (blenderAdd != null)
                //{
                //    blenderAdd.AddToBlender(DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<SporeItem>().Spore.Genome);
                //    DragAndDropHolderFSM.Instance.Drop();
                //}
                //else
                //    DragAndDropHolderFSM.Instance.Release();
                AddToBlender(DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<SporeItem>().Spore.Genome);
            }
            else
            {
                DragAndDropHolderFSM.Instance.Release();
            }
        }
    }
}
