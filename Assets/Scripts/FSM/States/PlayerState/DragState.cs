using UnityEngine;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public class DragState : State
    {
        public DragState(IFSMActions fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            _fsm.DraggedElement.GetComponent<Collider2D>().enabled = false;
            _fsm.DraggedElement.GetComponent<SpriteRenderer>().sortingLayerName = "DragLayer";
        }
        public override void Execute()
        {
            _fsm.UpdatePositionOfDraggedElement();
        }
        public override void ExitState()
        {
            _fsm.DraggedElement.GetComponent<Collider2D>().enabled = true;
            _fsm.DraggedElement.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            _fsm.DraggedElement.SetActive(false);
        }
        public override State GetNextState()
        {
            if (_fsm.IsDraggingInCanvas)
            {
                return new DragCanvasState(_fsm);
            }
            else if (!_fsm.IsDragging)
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
