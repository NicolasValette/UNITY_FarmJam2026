using TMPro;
using UnityEngine;

namespace FarmJam2026
{
    public class AppVersion : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _versionText;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _versionText.text = $"Version: {Application.version}";
        }
    }
}
