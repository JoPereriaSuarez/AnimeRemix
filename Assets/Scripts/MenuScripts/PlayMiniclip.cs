using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMiniclip : MonoBehaviour
{
    private AudioSource source;
    private Image im;

	void Awake()
	{
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.loop = false;

        im = GetComponent<Image>();
	}

    public void ChangeAudioClip(AudioClip clip)
    {
        CancelInvoke("RepeatAudio");
        source.clip = clip;

        InvokeRepeating("RepeatAudio", 0.1F, 18F);
    }

    public void ChangeImage(Sprite image)
    {
        im.sprite = image;
    }

    void RepeatAudio()
    {
        if(source.clip != null)
        {
            source.Play();
        }
    }
}
