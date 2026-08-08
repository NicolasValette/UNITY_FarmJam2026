using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class DragElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public SpriteRenderer Renderer { get; private set; }
        private float _time = 0;
        private bool _hadDraggingStart = false;
        public void OnBeginDrag(PointerEventData eventData)
        {
            _time = 0;
            _hadDraggingStart = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_hadDraggingStart)
            {
                _time += Time.deltaTime;
                if (_time > DragAndDropHolderFSM.Instance.TimeToDrag)
                {
                    _hadDraggingStart = true;
                    DragAndDropHolderFSM.Instance.RegisteredDraggedElement(this);
                }
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragAndDropHolderFSM.Instance.UnRegisteredDraggedElement();
        }
    }

    
}
