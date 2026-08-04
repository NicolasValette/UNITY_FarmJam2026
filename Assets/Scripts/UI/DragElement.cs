using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public class DragElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("BeginDrag");
            DragAndDropHolderFSM.Instance.RegisteredDraggedElement(gameObject);

        }

        public void OnDrag(PointerEventData eventData)
        {
            //gameObject.transform.position = eventData.position;
            //eventData.pointerCurrentRaycast.worldPosition
            //Debug.Log("Drag");
            // DragAndDropHolderFSM.Instance.SetDraggedObjectDelta(eventData.pointerCurrentRaycast.worldPosition);
            //transform.position = eventData.pointerCurrentRaycast.worldPosition;
           
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End Drag");
            DragAndDropHolderFSM.Instance.UnRegisteredDraggedElement();
            eventData.hovered.ForEach(x => Debug.Log(x.name));
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("ff");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
