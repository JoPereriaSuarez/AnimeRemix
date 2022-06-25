using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using static AnimeRemix.Core.BeatSync;

#pragma warning disable IDE0044, CS0649
namespace AnimeRemix.Core
{
    public class VideoController : MonoBehaviour
    {
        [SerializeField]
        private VideoPlayer videoPlayer;
        [SerializeField]
        private VideoClip videoClip;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip audioClip;
        [SerializeField]
        private AudioClip beatSFX;
        [SerializeField]
        private AudioClip measureSFX;

        [SerializeField]
        private uint bpm = 141;
        [SerializeField]
        private ushort measure = 4;
        [SerializeField]
        private float initialOffset = 0;

        public UnityEvent onQuaterBeat = new();
        public UnityEvent onMeasure = new();


        private BeatSync beatSync;
        private VideoPlayer.EventHandler onVideoPrepared;
        private TickDelegate onBeatTicked;
        private uint previousBeat;
        private uint previousMeasure;

        private void Awake()
        {
            beatSync = new(bpm, initialOffset, measure);
            audioSource.playOnAwake = false;
            audioSource.clip = audioClip;
            videoPlayer.playOnAwake = false;
            videoPlayer.clip = videoClip;
            onVideoPrepared = (source) =>
            {
                beatSync.Start();
                audioSource.PlayScheduled(beatSync.NextTick - initialOffset);
            };
            onBeatTicked = (dto) =>
            {
                    if(dto.bpm == 1)
                    {
                        videoPlayer.Play();
                    }
                    previousBeat = dto.bpm;
                    if(dto.bpm == 1 || dto.bpm % measure == 1)
                    {
                        print(dto.bpm);
                        previousMeasure = dto.measure;
                        GameObject _instance = new(nameof(measureSFX), typeof(AudioSource));
                        AudioSource _sfxInstance = _instance.GetComponent<AudioSource>();
                        _sfxInstance.playOnAwake = false;
                        _sfxInstance.volume = 0.5f;
                        _sfxInstance.clip = measureSFX;
                        _sfxInstance.PlayScheduled(dto.time + 1 + initialOffset);
                        return;
                    }
                    // GameObject instance = new(nameof(beatSFX), typeof(AudioSource));
                    // AudioSource sfxInstance = instance.GetComponent<AudioSource>();
                    // sfxInstance.playOnAwake = false;
                    // sfxInstance.volume = 0.5f;
                    // sfxInstance.clip = beatSFX;
                    // sfxInstance.PlayScheduled(dto.time + 1);
            };
        }

        private void Start()
        {
            previousBeat = 0;
            previousMeasure = 0;

            OnTicked += onBeatTicked;
            videoPlayer.prepareCompleted += onVideoPrepared;
            videoPlayer.Prepare();
        }

        private void Update()
        {
            beatSync.Tick();
        }
    }

}