using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimeRemix.Core
{
    public class BeatSync 
    {
        public delegate BpmDTO TickDelegate();
        public static BpmDTO OnTicked;

        private const ushort MAX_NOTE_RANGE = 16;        

        private uint beatCounter;
        private uint measureCounter;
        private uint tickCounter;

        internal readonly double bpm;
        internal readonly ushort measure;
        
        private double nextTick;
        private BpmDTO dto;

        internal BeatSync(uint bpm, ushort measure = 4)
        {
            this.bpm = 60.0D / (double)bpm;
            this.measure = measure;
            beatCounter = 1;
            measureCounter = 1;

            nextTick = AudioSettings.dspTime + (this.bpm / MAX_NOTE_RANGE);
            dto = new();
        }

        internal void Tick()
        {
            double time = AudioSettings.dspTime;
            if(time + (bpmRate / MAX_NOTE_RANGE) < nextTick)
                continue;
                
            tickCounter++;
            beatCounter += Convert.ToInt32(tickCounter % MAX_NOTE_RANGE == 0);
            measureCounter += Convert.ToInt32(beatCounter % measure == 0);

            nextTick += bpmRate / MAX_NOTE_RANGE;

            dto.bpm = beatCounter;
            dto.measure = measureCounter;
            dto.time = time;
            dto.tick = tickCounter;
            OnTicked?.Invoke(dto);
        }
    }
}
