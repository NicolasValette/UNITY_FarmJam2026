using FarmJam2026.Assets.Scripts.Genetics.Genes;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
            var variantData = _fsm.GetGenomeData().Genes.OfType<VariantGene>().FirstOrDefault().VariantData;
            
            _fsm.CanvasDraggedElement.GetComponent<Image>().sprite = variantData.MutadexColoredSprite;

            _fsm.CanvasDraggedElement.GetComponent<Image>().color = _fsm.GetGenomeData().Genes.OfType<ColorGene>().FirstOrDefault().Color;
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
            else
            {
                var genome = _fsm.GetGenomeData();
                _fsm.DraggedElement = GameObject.Instantiate(PrefabLibrary.Instance.SporeInventairePrefab, Vector3.zero, Quaternion.identity).GetComponent<DragElement>();
                var sporeItem = _fsm.DraggedElement.GetComponent<SporeItem>();
                sporeItem.Spore.Genome = new Genome { GenomeData = genome};
                sporeItem.Quantity = 1;
                _fsm.DraggedElement.GetComponentInChildren<SpriteRenderer>(false).color = genome.Genes.OfType<ColorGene>().FirstOrDefault().Color;
                _fsm.DraggedElement.transform.localScale *= 100;
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
