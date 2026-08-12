using UnityEngine;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public class PauseMenu : MonoBehaviour
    {

        [SerializeField]
        private GameObject _pausePanel;
        [SerializeField]
        private OptionMenu _optionMenu;
        private bool _isPaused = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _pausePanel.SetActive(false);
            _isPaused = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.escapeKey.wasReleasedThisFrame)
            {
                if (_isPaused)
                    Unpause();
                else
                    Pause();
            }
        }
        public void Pause()
        {
            _isPaused = true;
            _pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        public void Unpause()
        {
            _optionMenu.CloseOptionPanel();
            _isPaused = false;
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
        }
        public void SaveGameButton()
        {
            if (SaveGame.Instance != null)
                SaveGame.Instance.Save();
        }
    }
}
