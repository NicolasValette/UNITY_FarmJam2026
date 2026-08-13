using TMPro;
using UnityEngine;

namespace FarmJam2026
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField]
        private GameObject _messageBox;
        [SerializeField]
        private TMP_Text _text;

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
