using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class TestHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("HOVER DU BOUTON WESH");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("ON SE BARRE DU BOUTON WEEEEEEE");
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
