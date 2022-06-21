using UnityEngine;
using System.Collections;
using System;

public class MenuSongElement : MonoBehaviour, ISelectable
{
    public PlayMiniclip image;
    public AudioClip clip;
    public Sprite background;
    public string levelToGo;

    public void OnPress()
    {
        if(GameController.instance != null)
        {
            GameController.instance.LoadLevel(levelToGo);
        }
    }

    public void OnSelect()
    {
        image.ChangeImage(background);
        image.ChangeAudioClip(clip);
    }
}
