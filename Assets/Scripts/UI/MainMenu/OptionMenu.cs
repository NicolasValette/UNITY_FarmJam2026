using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmJam2026
{
    public class OptionMenu : MonoBehaviour
    {
        #region Reference
        [Header("Reference")]
        [SerializeField]
        private GameObject _mainButtonPanel;
        [SerializeField]
        private GameObject _optionPanel;
        #endregion
        #region Sound Option
        [Space]
        [Header("Sound Sliders")]
        [SerializeField]
        private Slider _masterVolumeSlider;
        [SerializeField]
        private TMP_Text _masterText;
        [SerializeField]
        private Slider _musicVolumeSlider;
        [SerializeField]
        private TMP_Text _musicText;
        [SerializeField]
        private Slider _sfxVolumeSlider;
        [SerializeField]
        private TMP_Text _sfxText;
        #endregion

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _mainButtonPanel.SetActive(true);
            _optionPanel.SetActive(false);
            _masterVolumeSlider.value = SoundManager.Instance.MasterVolume;
            _musicVolumeSlider.value = SoundManager.Instance.MusicVolume;
            _sfxVolumeSlider.value = SoundManager.Instance.SFXVolume;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OpenOptionPanel()
        {
            _optionPanel.SetActive(true);
            _mainButtonPanel.SetActive(false);
            _masterVolumeSlider.value = SoundManager.Instance.MasterVolume;
            _masterText.text = Mathf.RoundToInt((SoundManager.Instance.MasterVolume * 100)).ToString();
            _masterVolumeSlider.value = SoundManager.Instance.MasterVolume;
            _musicText.text = Mathf.RoundToInt((SoundManager.Instance.MusicVolume * 100)).ToString();
            _sfxVolumeSlider.value = SoundManager.Instance.SFXVolume;
            _sfxText.text = Mathf.RoundToInt((SoundManager.Instance.SFXVolume * 100)).ToString();

        }
        public void CloseOptionPanel()
        {
            _mainButtonPanel.SetActive(true);
            _optionPanel.SetActive(false);
        }
        public void SaveMasterVolume(float volume)
        {
            SoundManager.Instance.SetMasterVolume(volume);
            _masterText.text = Mathf.RoundToInt(volume * 100).ToString();
        }
        public void SaveMusicVolume(float volume)
        {
            SoundManager.Instance.SetMusicVolume(volume);
            _musicText.text = Mathf.RoundToInt(volume * 100).ToString();
        }
        public void SaveSFXVolume(float volume)
        {
            SoundManager.Instance.SetSFXVolume(volume);
            _sfxText.text = Mathf.RoundToInt(volume * 100).ToString();
        }
    }
}
