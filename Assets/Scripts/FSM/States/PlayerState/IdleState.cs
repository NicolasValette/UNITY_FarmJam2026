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
        }
        public override void Execute()
        {
        }
        public override void ExitState()
        {
        }
        public override State GetNextState()
        {
            return (_fsm.IsDragging) ? new DragState(_fsm) : null;
        }

    }
}
