using UnityEngine;

namespace AnimeRemix.Core
{
    public class BeatSync 
    {
        public delegate void TickDelegate(BpmDTO dto);
        public static event TickDelegate OnTicked;

        private const ushort MAX_NOTE_RANGE = 16;        

        private uint beatCounter;
        private uint measureCounter;
        private uint tickCounter;

        internal readonly double bpmRate;
        internal readonly ushort measure;
        
        public double NextTick { get; private set; }
        private BpmDTO dto;
        public bool HasStarted { get; private set; }
        private readonly float offset;
        internal BeatSync(uint bpm, float offset, ushort measure = 4)
        {
            bpmRate = 60.0D / (double)bpm;
            this.measure = measure;
            beatCounter = 0;
            measureCounter = 1;
            this.offset = offset;
            dto = new();
        }

        internal void Start()
        {
            NextTick = AudioSettings.dspTime + bpmRate;
            HasStarted = true;
        }
        internal void Tick()
        {
            if(!HasStarted)
                return;

            double time = AudioSettings.dspTime;
            if(time + 1.0 > NextTick)
            {
                beatCounter++;
                NextTick += bpmRate;
                OnTicked?.Invoke(new()
                {
                    bpm = beatCounter,
                    time = time
                });
            }
        }
    }
}
