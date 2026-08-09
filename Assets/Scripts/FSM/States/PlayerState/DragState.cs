using UnityEngine;
using UnityEngine.UI;

namespace FarmJam2026
{
    public class DragState : State
    {
        private int enterLayer;
        public DragState(IFSMActions fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            _fsm.HasReleased = false;
            var coll = _fsm.DraggedElement.gameObject.GetComponent<Collider2D>();
            if (coll != null)
                coll.enabled = false;
            enterLayer = _fsm.DraggedElement.gameObject.layer;
            _fsm.DraggedElement.gameObject.layer = LayerMask.GetMask("DragLayer");
        }
        public override void Execute()
        {
            _fsm.UpdatePositionOfDraggedElement();
        }
        public override void ExitState()
        {
            var coll = _fsm.DraggedElement.gameObject.GetComponent<Collider2D>();
            if (coll != null)
                _fsm.DraggedElement.gameObject.GetComponent<Collider2D>().enabled = true;
            _fsm.DraggedElement.gameObject.layer = enterLayer;
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
            if (_fsm.ObjectType == DragTypeObject.SporeFromMutadex)
            {
                GameObject.Destroy(_fsm.DraggedElement.gameObject);
                var element = _fsm.DraggedElementinCanvas.GetComponent<MutadexElement>();
                element.Page.RemoveMushroom(_fsm.DraggedElementinCanvas.GetComponent<Image>(), element.Genome);
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
