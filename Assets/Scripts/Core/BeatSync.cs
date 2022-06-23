using System;
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
        
        public double nextTick { get; private set; }
        private BpmDTO dto;
        public bool HasStarted { get; private set; }

        internal BeatSync(uint bpm, ushort measure = 4)
        {
            bpmRate = 60.0D / (double)bpm;
            this.measure = measure;
            beatCounter = 0;
            measureCounter = 1;
            dto = new();
        }

        internal void Start()
        {
            nextTick = AudioSettings.dspTime + (this.bpmRate / MAX_NOTE_RANGE);
            HasStarted = true;
        }
        internal void Tick()
        {
            if(!HasStarted)
                return;

            double time = AudioSettings.dspTime;
            if(time + 1 + (bpmRate / MAX_NOTE_RANGE) < nextTick)
                return;
                
            tickCounter++;
            if(tickCounter % MAX_NOTE_RANGE == 0)
            {
                beatCounter++;
                if(beatCounter % measure == 0)
                    measureCounter++;
            }
            nextTick += bpmRate / MAX_NOTE_RANGE;

            dto.bpm = beatCounter;
            dto.measure = measureCounter;
            dto.time = time;
            dto.tick = tickCounter;
            OnTicked?.Invoke(dto);
        }
    }
}
