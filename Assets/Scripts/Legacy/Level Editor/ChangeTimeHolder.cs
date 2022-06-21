using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ChangeTimeHolder : BeatReactor
{
    InputField field;
    public AudioTracker audioTracker;
    public Text displayText;
    public Slider slide;

    float songTime
    { get { return AudioTracker.songTime; } }
    uint minutes;
    float seconds;

    private void Awake()
    {
        field = GetComponent<InputField>();
    }

    public void ChangeTime()
    {
        float time;
        if(float.TryParse(field.text, out time))
        {
            audioTracker.ChangeSongTime(time);
        }
    }

    public void  DisplayTimeInSeconds()
    {
        if(!displayText)
        { return; }
        minutes = (uint)( songTime / 60 );
        seconds = ( songTime % 60 );

        displayText.text = minutes.ToString("00") +":" + seconds.ToString("00.000");
    }

    protected override void OnTimeChecker(float delta)
    {
        DisplayTimeInSeconds();
        /*
        if (slide)
        {
            slide.value = songTime / audioTracker.GetClipLength();
            slide.Select();
        }
        */
    }

    public void ChangeSliderTime()
    {
        audioTracker.ChangeSongTime(slide.value * audioTracker.GetClipLength());
    }
}
