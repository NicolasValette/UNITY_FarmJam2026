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
        }
        public override void Execute()
        {
            _fsm.UpdatePositionOfCanvasDraggedElement();
        }
        public override void ExitState()
        {
            _fsm.CanvasDraggedElement.GetComponent<SortingGroup>().sortingLayerName = "Default";
            _fsm.CanvasDraggedElement.SetActive(false);
        }
        public override State GetNextState()
        {
            return null;
        }
    }
}
