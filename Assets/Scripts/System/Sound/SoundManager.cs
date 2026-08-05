using UnityEngine;

namespace FarmJam2026
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set;  }

        public MusicDictionary MusicDico;
        public SFXDictionary SfxDico;

        #region Options
        public float MusicVolume = 0.8f;
        public float SfxVolume = 1f;
        public ESoundMusic DefaultMusic => ESoundMusic.Day;
        #endregion

        [SerializeField]
        private AudioSource _musicSource;
        [SerializeField]
        private AudioSource _sfxSource;

        public void Awake()
        {
            Instance = this;
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
           if (_musicSource == null)
            {
                Debug.LogError("Missing Audio source for music, adding one");
                _musicSource = gameObject.AddComponent<AudioSource>();
                _musicSource.loop = true;
            }
            _musicSource.volume = MusicVolume;
            PlayMusic(DefaultMusic);

            if (_sfxSource == null)
            {
                Debug.LogError("Missing Audio source for SFX, adding one");
                _sfxSource = gameObject.AddComponent<AudioSource>();
            }
            _sfxSource.volume = SfxVolume;
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

            var curTime = _musicSource.clip == null ? 0f : _musicSource.time;
            _musicSource.clip = clip;

            if (smoothChange)
                _musicSource.time = curTime % clip.length;

            _musicSource.Play();
        }
        public void SetMusicVolume(float volume)
        {
            _musicSource.volume = Mathf.Clamp01(volume);
        }

        public void PlaySFX(ESoundSFX sfx)
        {
            var sfxId = (int)sfx;
            var clip = SfxDico.Audios[sfxId];

            if (_sfxSource.clip == clip && _sfxSource.isPlaying)
                return;

            _sfxSource.PlayOneShot(clip);
        }
        public void SetSFXVolume(float volume)
        {
            _sfxSource.volume = Mathf.Clamp01(volume);
        }
    }
}
