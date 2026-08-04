using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class DropZone : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
