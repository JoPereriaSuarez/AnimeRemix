using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RankingButtons : MonoBehaviour, ISelectable
{
    public MenuButtonType type;

    public void OnPress()
    {
        switch (type)
        {
            case MenuButtonType.BackToMenu:
            if (GameController.instance != null)
            {
                GameController.instance.LoadLevel("Menu");
            }
            break;

            case MenuButtonType.Restart:
            if(GameController.instance != null)
            {
                string level = PlayerPrefs.GetString("GameLevel", "Menu");
                GameController.instance.LoadLevel(level);
            }
            break;

        }
    }

    public void OnSelect()
    {
        return;
    }
}
