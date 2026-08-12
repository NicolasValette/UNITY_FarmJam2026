using UnityEngine;

namespace FarmJam2026
{
    public class IdleState : State
    {
        public IdleState(IFSMActions fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            _fsm.HasDrop = false;
            _fsm.HasReleased = false;
            _fsm.IsDraggingInCanvas = false;
            _fsm.IsDragging = false;
            _fsm.ObjectType = DragTypeObject.None;
            _fsm.DraggedElement = null;
        }
        public override void Execute()
        {
        }
        public override void ExitState()
        {
        }
        public override State GetNextState()
        {

            if ((_fsm.IsDraggingInCanvas))
            {
                return new DragCanvasState(_fsm);
            }
            else if ((_fsm.IsDragging))
            {
                return new DragState(_fsm);
            }
            else
            {
                return null;
            }
        }

    }
}
