using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Overgrown.Audio;
using Overgrown.Utility;

namespace Overgrown.GameManager
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioClip bgm;
        [SerializeField] private AudioClip clipLose;
        [SerializeField] private AudioClip clipWin;

        private SoundEffect[] soundEffects;
        private AudioSource[] audioSources;
        private AudioSource soundSource;
        private AudioSource musicSource;
        private AudioSource ambienceSource;

        public AudioMixer audioMixer;

        private float masterVolume = 0.2f;
        private float soundVolume = 1f;
        private float musicVolume = 1f;
        private float ambienceVolume = 1f;

        private const string GroupMaster = "Master";
        private const string GroupSound = "Sound";
        private const string GroupMusic = "Music";
        private const string GroupAmbience = "Ambience";

        private FloatEventChannelSO OnMasterVolumeChanged;
        private FloatEventChannelSO OnSoundVolumeChanged;
        private FloatEventChannelSO OnMusicVolumeChanged;
        private FloatEventChannelSO OnAmbienceVolumeChanged;
        private VoidEventChannelSO OnLevelLose;
        private VoidEventChannelSO OnLevelWin;

        public float MasterVolume { get => masterVolume; set => UpdateMasterVolume(value); }
        public float SoundVolume { get => soundVolume; set => UpdateSoundVolume(value); }
        public float MusicVolume { get => musicVolume; set => UpdateMusicVolume(value); }
        public float AmbienceVolume { get => ambienceVolume; set => UpdateAmbienceVolume(value); }
        public void PlaySound(AudioClip clipToPlay) => soundSource.PlayOneShot(clipToPlay);
        public bool PlayingAmbience { get => ambienceSource.isPlaying; }
        public AudioClip CurrentBGM { get => musicSource.clip; }

        #region Unity Methods
        private void Awake()
        {
            audioSources = GetComponents<AudioSource>();
            soundSource = audioSources[0];
            musicSource = audioSources[1];
            ambienceSource = audioSources[2];

            OnMasterVolumeChanged = Resources.Load<FloatEventChannelSO>("Channels/OnMasterVolumeChanged");
            OnSoundVolumeChanged = Resources.Load<FloatEventChannelSO>("Channels/OnSoundVolumeChanged");
            OnMusicVolumeChanged = Resources.Load<FloatEventChannelSO>("Channels/OnMusicVolumeChanged");
            OnAmbienceVolumeChanged = Resources.Load<FloatEventChannelSO>("Channels/OnAmbienceVolumeChanged");
            OnLevelLose = Resources.Load<VoidEventChannelSO>("Channels/OnLevelLose");
            OnLevelWin = Resources.Load<VoidEventChannelSO>("Channels/OnLevelWin");

        }

        private void Start()
        {
            if (GameManager.Instance.AudioManager == null)
            {
                GameManager.Instance.AudioManager = this;
            }

            soundEffects = Resources.LoadAll("Sound Effects", typeof(SoundEffect)).Cast<SoundEffect>().ToArray();

            if (bgm)
            {
                PlayMusic(bgm, true);
            }
            musicSource.loop = true;
            ambienceSource.loop = true;

            
            UpdateMasterVolume(masterVolume);
            UpdateSoundVolume(soundVolume);
            UpdateMusicVolume(musicVolume);
            UpdateAmbienceVolume(ambienceVolume);

            OnMasterVolumeChanged.OnEventRaised += UpdateMasterVolume;
            OnSoundVolumeChanged.OnEventRaised += UpdateSoundVolume;
            OnMusicVolumeChanged.OnEventRaised += UpdateMusicVolume;
            OnAmbienceVolumeChanged.OnEventRaised += UpdateAmbienceVolume;
            OnLevelLose.OnEventRaised += PlayLoseClip;
            OnLevelWin.OnEventRaised += PlayWinClip;

            MasterVolume = GameManager.Instance.Config.MasterVolume;
            SoundVolume = GameManager.Instance.Config.SoundVolume;
            MusicVolume = GameManager.Instance.Config.MusicVolume;
        }

        private void OnDestroy()
        {
            OnMasterVolumeChanged.OnEventRaised -= UpdateMasterVolume;
            OnSoundVolumeChanged.OnEventRaised -= UpdateSoundVolume;
            OnMusicVolumeChanged.OnEventRaised -= UpdateMusicVolume;
            OnAmbienceVolumeChanged.OnEventRaised -= UpdateAmbienceVolume;
            OnLevelLose.OnEventRaised -= PlayLoseClip;
            OnLevelWin.OnEventRaised -= PlayWinClip;
        }

        #endregion

        #region Public Methods

        public void PlayMusic(AudioClip clipToPlay, bool fade = false)
        {
            if (musicSource.clip == clipToPlay) { return; }

            if (fade)
                StartCoroutine(StartFade(GroupMusic, 1f, 1f));
            musicSource.clip = clipToPlay;
            musicSource.Play();
        }

        public void StopMusic()
        {
            if (!musicSource.isPlaying) { return; }
            musicSource.Stop();
        }

        public void PlayAmbience(SoundType type, bool fade = false)
        {
            SoundEffect soundEffectToPlay = null;
            foreach (SoundEffect sound in soundEffects)
            {
                if (sound.type == type)
                {
                    soundEffectToPlay = sound;
                }
            }
            if (soundEffectToPlay != null)
            {
                if (fade)
                    StartCoroutine(StartFade(GroupAmbience, 1f, 1f));
                ambienceSource.clip = soundEffectToPlay.GetRandomClip();
                ambienceSource.Play();
            }
        }
        public void PlayAmbience(SoundType type, AudioSource source, bool fade = false)
        {
            SoundEffect soundEffectToPlay = null;
            foreach (SoundEffect sound in soundEffects)
            {
                if (sound.type == type)
                {
                    soundEffectToPlay = sound;
                }
            }
            if (soundEffectToPlay != null)
            {
                source.volume = ambienceVolume * masterVolume;
                source.clip = soundEffectToPlay.GetRandomClip();
                source.Play();
            }
        }

        public void StopAmbience()
        {
            if (!ambienceSource.isPlaying) { return; }
            ambienceSource.Stop();
        }

        public void PlaySound(SoundType type)
        {
            SoundEffect soundEffectToPlay = null;
            foreach (SoundEffect sound in soundEffects)
            {
                if (sound.type == type)
                {
                    soundEffectToPlay = sound;
                }
            }

            if (soundEffectToPlay != null)
                soundSource.PlayOneShot(soundEffectToPlay.GetRandomClip());

        }

        public void PlaySound(SoundType type, AudioSource source)
        {
            SoundEffect soundEffectToPlay = null;
            foreach (SoundEffect sound in soundEffects)
            {
                if (sound.type == type)
                {
                    soundEffectToPlay = sound;
                }
            }

            if (soundEffectToPlay != null)
            {
                source.volume = soundVolume * masterVolume;
                source.clip = soundEffectToPlay.GetRandomClip();
                source.Play();
            }
        }


        #endregion

        #region Private Methods

        private void UpdateMasterVolume(float volume)
        {
            masterVolume = volume;
            soundSource.volume = soundVolume * masterVolume;
            musicSource.volume = musicVolume * masterVolume;
            ambienceSource.volume = ambienceVolume * masterVolume;
            GameManager.Instance.Config.MasterVolume = volume;
        }

        private void UpdateSoundVolume(float volume)
        {
            soundVolume = volume;
            soundSource.volume = soundVolume * masterVolume;
            GameManager.Instance.Config.SoundVolume = volume;
        }

        private void UpdateMusicVolume(float volume)
        {
            musicVolume = volume;
            musicSource.volume = musicVolume * masterVolume;
            GameManager.Instance.Config.MusicVolume = volume;
        }

        private void UpdateAmbienceVolume(float volume)
        {
            ambienceVolume = volume;
            ambienceSource.volume = ambienceVolume * masterVolume;
            GameManager.Instance.Config.AmbienceVolume = volume;
        }

        private IEnumerator StartFade(string audioGroup, float duration, float targetVolume)
        {
            float currentTime = 0;
            audioMixer.GetFloat(audioGroup, out var currentVol);
            currentVol = Mathf.Pow(10, currentVol / 20);
            float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
                audioMixer.SetFloat(audioGroup, Mathf.Log10(newVol) * 20);
                yield return null;
            }
        }

        private void PlayLoseClip()
        {
            musicSource.Stop();
            musicSource.PlayOneShot(clipLose);
            musicSource.PlayDelayed(clipLose.length);
            musicSource.Play();
        }

        private void PlayWinClip()
        {
            musicSource.Stop();
            musicSource.PlayOneShot(clipWin);
            musicSource.PlayDelayed(clipWin.length);
            musicSource.Play();
        }

        #endregion
    }
}

