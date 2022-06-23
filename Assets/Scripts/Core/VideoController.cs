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
        private uint bpm = 141;
        [SerializeField]
        private ushort measure = 4;

        public UnityEvent onQuaterBeat = new();
        public UnityEvent onMeasure = new();


        private BeatSync beatSync;
        private VideoPlayer.EventHandler onVideoPrepared;
        private TickDelegate onBeatTicked;
        private uint previousBeat;
        private uint previousMeasure;

        private void Awake()
        {
            beatSync = new(141, measure);
            audioSource.playOnAwake = false;
            audioSource.clip = audioClip;
            videoPlayer.playOnAwake = false;
            videoPlayer.clip = videoClip;
            onVideoPrepared = (source) =>
            {
                beatSync.Start();
                audioSource.PlayScheduled(beatSync.nextTick);
                source.Play();
            };
            onBeatTicked = (dto) =>
            {
                if(dto.bpm > previousBeat)
                {
                    previousBeat = dto.bpm;
                    if(dto.measure > previousMeasure)
                    {
                        previousMeasure = dto.measure;
                        onMeasure?.Invoke();
                        return;
                    }
                    onQuaterBeat?.Invoke();
                }
            };
        }

        private void Start()
        {
            previousBeat = 1;
            previousMeasure = 1;

            BeatSync.OnTicked += onBeatTicked;
            videoPlayer.prepareCompleted += onVideoPrepared;
            videoPlayer.Prepare();
        }

        private void Update()
        {
            beatSync.Tick();
        }
    }

}