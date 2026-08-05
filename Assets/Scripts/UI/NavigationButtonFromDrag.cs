using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FarmJam2026
{
    [RequireComponent(typeof(Button))]
    public class NavigationButtonFromDrag : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField]
        private UnityEvent action;
      

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("aaaaaaaaaaaaaaa");
            if (DragAndDropHolderFSM.Instance.IsDraggingInCanvas && DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.Mushroom)
            {
                action.Invoke();
            }
        }
    }
}
