using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace FarmJam2026
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField]
        private GameObject _messageBox;
        [SerializeField]
        private TMP_Text _text;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void DisplayMessageBox(string message)
        {
            _text.text = message;
            _messageBox.SetActive(true);
        }
        public void HideMessageBox()
        {
            _messageBox.SetActive(false);
        }
    }
}
