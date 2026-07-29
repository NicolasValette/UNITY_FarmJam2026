using UnityEngine;

namespace FarmJam2026
{
    public class OpenGeneBook : MonoBehaviour
    {
        [SerializeField]
        private GameObject _geneBookGO;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _geneBookGO.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OpenMenu()
        {
            _geneBookGO.SetActive(true);
            EventManager.TriggerEvent(EventManager.Events.OnUIMenuOpen);
        }
        public void CloseMenu()
        {
            _geneBookGO.SetActive(false);
            EventManager.TriggerEvent(EventManager.Events.OnUIMenuClose);
        }
    }
}
