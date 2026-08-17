using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    public class QuitButton : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
