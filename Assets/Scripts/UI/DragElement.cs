using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public class DragElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [field: SerializeField]
        public SpriteRenderer Renderer { get; private set; }
        public void OnBeginDrag(PointerEventData eventData)
        {
            DragAndDropHolderFSM.Instance.RegisteredDraggedElement(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragAndDropHolderFSM.Instance.UnRegisteredDraggedElement();
        }
    }

    
}
