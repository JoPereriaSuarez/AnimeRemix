using UnityEngine;
using System.Collections;
using System;

// Reproduce, Pausa y Resume el audioClip y Movie
// output: isPlaying, songTime, playMovie
[RequireComponent(typeof(AudioSource))]
public class AudioTracker : BeatReactor
{
    public static float songTime
    {
        get { return instance._songTime; }
    }
    public static AudioTracker instance;

    public bool isPlaying { get { return source.isPlaying; } }
    public bool playMovie = true;
    bool isPaused = false;

    public float _songTime { get; private set; }
    AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] PlayMovie movie;

    [SerializeField] bool isTesting;
    [SerializeField] KeyCode playKey = KeyCode.Space;
    [SerializeField] KeyCode stopKey = KeyCode.P;

    private void Awake()
    {
        instance = this;   
    }

    protected override void Start()
    {
        _songTime = 0.0F;
        source = GetComponent<AudioSource>();
        source.clip = clip;
        base.Start();
    }

    public void PlayResumeAudio()
    {
        if (source.isPlaying)
        { return; }

        if(isPaused)
        {
            source.UnPause();
        }
        else
        {
            source.PlayScheduled(0.832D);
            isPaused = false;
        }
        //if (playMovie)
        //{ 
        print("movie NAme " + movie.name);
        movie.movie.Stop();
        movie.movie.Play();
        //}

    }
    public void StopAudio()
    {
        source.time = 0.0F;
        source.Stop();
        if (playMovie)
        { movie.movie.Stop(); }
        _songTime = 0.0F;
        isPaused = false;
    }
    public void PauseAudio()
    {
        source.Pause();
        isPaused = true;
        if (playMovie)
        { movie.movie.Pause(); }
    }

    // Input Check
    void Update()
    {
        if(!isTesting)
        { return; }

        if(Input.GetKeyDown(playKey) && source.isPlaying)
        { PauseAudio(); }
        else if(Input.GetKeyDown(playKey))
        { PlayResumeAudio(); }
        else if(Input.GetKeyDown(stopKey))
        { StopAudio(); }
    }

    protected override void OnTimeChecker(float delta)
    {
        try
        {
            if (!instance.source.isPlaying)
            {
                return;
            }

            _songTime += delta;
        }
        catch(MissingReferenceException)
        {
            instance = FindObjectOfType<AudioTracker>();
            instance.playMovie = true;
        }
    }

    public void ChangeSongTime(float t)
    {
        _songTime = t;
        source.time = t;
    }

    public float GetClipLength()
    {
        return clip.length;
    }
}
