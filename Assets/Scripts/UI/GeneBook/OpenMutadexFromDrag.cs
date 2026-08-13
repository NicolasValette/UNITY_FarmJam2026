using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class OpenMutadexFromDrag : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private GeneBook _geneBook;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (DragAndDropHolderFSM.Instance.IsDragging && DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.Mushroom)
            {
                Debug.Log("Open with Dragging ?");
                Mushroom mush = DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<Mushroom>();
                _geneBook.OpenMenuOnSpecificPage(mush.Genome.GenomeData);
                DragAndDropHolderFSM.Instance.SwitchDropMode();
            }
            if (DragAndDropHolderFSM.Instance.IsDragging && DragAndDropHolderFSM.Instance.IsDraggingInCanvas && DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.SporeFromMutadex)
            {
                Debug.Log("Close with Dragging ?");
                _geneBook.CloseMenu();
                DragAndDropHolderFSM.Instance.SwitchDropMode();
            }
        }

    }
}
