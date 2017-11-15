using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mantiene la actualizacion del sistema de sonido
// Envia el evento a todos los subscriptores cuando se actualiza.
public class BeatTracker : MonoBehaviour
{
    public delegate void TimeChangedEventHandler(float delta);
    public static event TimeChangedEventHandler OnTimeChanged;
    
    public static float soundDeltaTime { get; private set; }

    double currentFrameTime;
    double previousFrameTime;

    private void Start()
    {
        currentFrameTime = AudioSettings.dspTime;
        previousFrameTime = AudioSettings.dspTime;
    }

    private void Update()
    { 
        currentFrameTime = AudioSettings.dspTime;
        if (currentFrameTime != previousFrameTime)
        {
            soundDeltaTime = (float)( currentFrameTime - previousFrameTime );
            previousFrameTime = currentFrameTime;
           //print(soundDeltaTime);
            if(OnTimeChanged != null)
            {
                OnTimeChanged(soundDeltaTime);
            }
        }
    }

}
