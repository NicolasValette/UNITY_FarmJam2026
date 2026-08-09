using UnityEngine;

namespace FarmJam2026
{
    public class DragState : State
    {
        public DragState(IFSMActions fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            _fsm.HasReleased = false;
            _fsm.DraggedElement.gameObject.GetComponent<Collider2D>().enabled = false;
            _fsm.DraggedElement.gameObject.layer = LayerMask.GetMask("DragLayer");
        }
        public override void Execute()
        {
            _fsm.UpdatePositionOfDraggedElement();
        }
        public override void ExitState()
        {
            _fsm.DraggedElement.gameObject.GetComponent<Collider2D>().enabled = true;
            _fsm.DraggedElement.gameObject.layer = LayerMask.GetMask("Default");
            _fsm.DraggedElement.transform.position = _fsm.InitialPosition;
    
            if (!_fsm.HasReleased)
            {
                //_fsm.DraggedElement.gameObject.SetActive(false);
               
                
                //TODO fix d&d
                //if (_fsm.IsDraggingInCanvas)
                //{
                //    _fsm.CanvasDraggedElement.GetComponent<Image>().sprite = _fsm.DraggedElement.Renderer.sprite;
                //    _fsm.CanvasDraggedElement.GetComponent<Image>().color = _fsm.DraggedElement.GetComponent<SpriteRenderer>().color;
                //}
            }
           

                
        }
        public override State GetNextState()
        {
            if (_fsm.IsDraggingInCanvas)
            {
                return new DragCanvasState(_fsm);
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
