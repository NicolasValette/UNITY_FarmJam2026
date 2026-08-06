using UnityEngine;
using UnityEngine.Audio;

namespace FarmJam2026
{
    public class SoundManager : MonoBehaviour
    {
        public enum MixerGroup
        {
            Master,
            Music,
            SFX
        }
        public static SoundManager Instance { get; private set;  }

        public MusicDictionary MusicDico;
        public SFXDictionary SfxDico;

        #region Options
        public ESoundMusic DefaultMusic => ESoundMusic.Day;
        #endregion

        [SerializeField]
        private AudioSource _musicSource;
        [SerializeField]
        private AudioSource _sfxSource;
        [SerializeField]
        private AudioMixer _audioMixer;

        public float MasterVolume { get; private set; }
        public float SFXVolume { get; private set; }
        public float MusicVolume { get; private set; }

        public void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            InitAudioSources();
        }

        public void OnEnable()
        {
            EventManager.StartListening(EventManager.Events.OnStartDay, () => PlayMusic(ESoundMusic.Day, true));
            EventManager.StartListening(EventManager.Events.OnStartNight, () => PlayMusic(ESoundMusic.Night, true));
        }

        public void OnDisable()
        {
            EventManager.StopListening(EventManager.Events.OnStartDay, () => PlayMusic(ESoundMusic.Day, true));
            EventManager.StopListening(EventManager.Events.OnStartNight, () => PlayMusic(ESoundMusic.Night, true));
        }

        private void InitAudioSources()
        {
            if (_audioMixer == null)
            {
                Debug.LogError("Missing Audio Mixer Reference in Sound Manager", gameObject);
            }
            MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

            if (_musicSource == null)
            {
                Debug.LogError("Missing Audio source for music, adding one");
                _musicSource = gameObject.AddComponent<AudioSource>();
                _musicSource.loop = true;
            }
            PlayMusic(DefaultMusic);

            if (_sfxSource == null)
            {
                Debug.LogError("Missing Audio source for SFX, adding one");
                _sfxSource = gameObject.AddComponent<AudioSource>();
            }
            SetMasterVolume(MasterVolume);
            SetMusicVolume(MusicVolume);
            SetSFXVolume(SFXVolume);
        }

        public void PlayMusic(ESoundMusic music, bool smoothChange = false)
        {
            var musicId = (int)music;
            var clip = MusicDico.Audios[musicId];
            if (clip == null)
            {
                Debug.LogError($"No music for {music}.");
                return;
            }

            if (_musicSource == null || _musicSource.clip == clip && _musicSource.isPlaying)
                return;

            var curTime = _musicSource.time;
            _musicSource.clip = clip;

            if (smoothChange)
                _musicSource.time = curTime % clip.length;

            _musicSource.Play();
        }

        public void PlaySFX(ESoundSFX sfx)
        {
            var sfxId = (int)sfx;
            var clip = SfxDico.Audios[sfxId];

            if (_sfxSource.clip == clip && _sfxSource.isPlaying)
                return;

            _sfxSource.PlayOneShot(clip);
        }
        #region Volume Settings
        private void SetVolume(MixerGroup group, float volume)
        {
            string parameter = "";
            if (group == MixerGroup.Master)
            {
                MasterVolume = volume;
                parameter = "MasterVolume";
            }
            else if (group == MixerGroup.Music)
            {
                MusicVolume = volume;
                parameter = "MusicVolume";
            }
            else if (group == MixerGroup.SFX)
            {
                SFXVolume = volume;
                parameter = "SFXVolume";
            }
            var vol = (volume == 0) ? -80 : Mathf.Log10(volume) * 20;
            _audioMixer.SetFloat(parameter, (volume == 0)?-80:Mathf.Log10(volume) * 20);
            
            PlayerPrefs.SetFloat(parameter, volume);
        }
        public void SetMasterVolume(float volume) => SetVolume(MixerGroup.Master, volume);
        public void SetMusicVolume(float volume) => SetVolume(MixerGroup.Music, volume);
        public void SetSFXVolume(float volume) => SetVolume(MixerGroup.SFX, volume);
        #endregion
    }
}
