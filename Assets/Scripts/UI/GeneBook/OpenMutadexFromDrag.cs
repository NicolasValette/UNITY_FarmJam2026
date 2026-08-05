using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace FarmJam2026
{
    public class OpenMutadexFromDrag : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private GeneBook _geneBook;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            Debug.Log("Pointer Enter");
            if (DragAndDropHolderFSM.Instance.IsDragging && DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.Mushroom)
            {
                Debug.Log("Open with Dragging ?");
                _geneBook.OpenMenu();
                DragAndDropHolderFSM.Instance.SwitchDropMode();
            }
        }

    }
}
