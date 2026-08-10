

using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class DragElementInCanvas : DragElement
    {
        public override void OnBeginDrag(PointerEventData eventData)
        {
            DragAndDropHolderFSM.Instance.RegisteredCanvasDraggedElement(this);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}
