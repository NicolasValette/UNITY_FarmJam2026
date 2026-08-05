using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class Blender : MonoBehaviour, IDropHandler
    {
        public List<Genome> Content { get; private set; } = new List<Genome>();

        public void OnDrop(PointerEventData eventData)
        {
            
            if (DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.InventorySpore)
            {
                var blenderAdd = gameObject.GetComponentInChildren<BlenderAdd>();
                if (blenderAdd != null)
                {
                    blenderAdd.AddToBlender(DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<SporeItem>().Spore.Genome);
                    DragAndDropHolderFSM.Instance.Drop();
                }
                else
                    DragAndDropHolderFSM.Instance.Release();
            }
            else
            {
                DragAndDropHolderFSM.Instance.Release();
            }
        }
    }
}
