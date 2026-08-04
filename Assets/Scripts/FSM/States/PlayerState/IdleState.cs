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
