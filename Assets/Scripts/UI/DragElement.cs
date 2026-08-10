using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;

namespace FarmJam2026
{
    public class DragElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public SpriteRenderer Renderer { get; private set; }
        private float _time = 0;
        private bool _hadDraggingStart = false;
        private Vector2 _startingPos;
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _time = 0;
            _hadDraggingStart = false;
            _startingPos = eventData.position;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!_hadDraggingStart)
            {
                if (DragAndDropHolderFSM.Instance.type == DragAndDropHolderFSM.DragType.Time)
                {

                    _time += Time.deltaTime;
                    if (_time > DragAndDropHolderFSM.Instance.TimeToDrag)
                    {
                        _hadDraggingStart = true;
                        DragAndDropHolderFSM.Instance.RegisteredDraggedElement(this);
                    }

                }
                else// if(DragAndDropHolderFSM.Instance.type == DragAndDropHolderFSM.DragType.Distance)
                {
                    Vector2 pos = eventData.position;
                    float dis = (pos - _startingPos).sqrMagnitude;
                    Debug.Log("dis = " + dis);
                    if (dis > DragAndDropHolderFSM.Instance.DistanceToDrag)
                    {
                        _hadDraggingStart = true;
                        DragAndDropHolderFSM.Instance.RegisteredDraggedElement(this);
                    }
                }
            }
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            DragAndDropHolderFSM.Instance.UnRegisteredDraggedElement();
        }
    }

    
}
