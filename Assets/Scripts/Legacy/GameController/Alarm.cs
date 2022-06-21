using UnityEngine;
using System.Collections;
using System;

public class Alarm : BeatReactor
{
    bool isSetup;
    float timeCheck;
    float targetTime;

    public bool isReady { get; private set; }
    bool destroy;

    public void Initialize(float t, bool destroy = true)
    {
        targetTime = t;
        timeCheck = 0.0F;
        isSetup = true;
        this.destroy = destroy;
    }

    protected override void OnTimeChecker(float delta)
    {
        if (!isSetup)
        { return; }
        timeCheck += delta;
        if(timeCheck >= targetTime)
        {
//            print("TIME ALARM " + timeCheck);
            isReady = true;
            isSetup = false;
            if(destroy)
            {
                GameObject.Destroy(this);
            }
        }
    }

}
