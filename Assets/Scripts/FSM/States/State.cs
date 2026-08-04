using UnityEngine;

namespace FarmJam2026
{
    public abstract class State
    {
        protected IFSMActions _fsm;

        public State(IFSMActions fsm)
        {
            _fsm = fsm;
        }
        public abstract void EnterState();
        public abstract void Execute();
        public abstract void ExitState();
        public abstract State GetNextState();

        public override string ToString()
        {
            return this.GetType().Name;
        }

    }

}
