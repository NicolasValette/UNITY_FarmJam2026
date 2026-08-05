using UnityEngine;
using UnityEngine.Rendering;

namespace FarmJam2026
{
    public class DragCanvasState : State
    {
        public DragCanvasState(IFSMActions fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            _fsm.CanvasDraggedElement.SetActive(true);
            _fsm.CanvasDraggedElement.GetComponent<SortingGroup>().sortingLayerName= "DragLayer";
            _fsm.HasReleased = false;
        }
        public override void Execute()
        {
            _fsm.UpdatePositionOfCanvasDraggedElement();
        }
        public override void ExitState()
        {
            _fsm.CanvasDraggedElement.GetComponent<SortingGroup>().sortingLayerName = "Default";
            _fsm.CanvasDraggedElement.SetActive(false);
            _fsm.IsDraggingInCanvas = false;
            if (_fsm.HasDrop)
            {
                GameObject.Destroy(_fsm.DraggedElement.gameObject);
            }
            else if (_fsm.HasReleased)
            {
                _fsm.DraggedElement.transform.position = _fsm.InitialPosition;
                _fsm.DraggedElement.gameObject.SetActive(true);
            }
        }
        public override State GetNextState()
        {
            if (!_fsm.IsDraggingInCanvas && _fsm.IsDragging)
            {
                return new DragState(_fsm);
            }
            else if (!_fsm.IsDraggingInCanvas && !_fsm.IsDragging)
            {
                return new IdleState(_fsm);
            }
            else if (_fsm.HasDrop || _fsm.HasReleased)
            {
                return new IdleState(_fsm);
            }
            else
            {
                return null;
            }
        }
    }
}
