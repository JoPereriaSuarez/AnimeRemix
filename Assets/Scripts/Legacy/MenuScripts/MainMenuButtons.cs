using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuButtonType
{
    Start,
    Credits,
    Exit,
    Restart,
    BackToMenu,
    BakcToMainMenu,
}

public class MainMenuButtons : MonoBehaviour, ISelectable
{
    public MenuButtonType type;

    public void OnPress()
    {
        switch (type)
        {
            case MenuButtonType.Exit:
            Application.Quit();
            break;
            case MenuButtonType.Start:
            GameController.instance.LoadLevel("Menu_Canciones");
            break;
            case MenuButtonType.Credits:
            print("SHOW CREDITS");
            MainMenuElements _menu = FindObjectOfType<MainMenuElements>();
            _menu.menu.gameObject.SetActive(false);
            _menu.credit.gameObject.SetActive(true);
            break;
            case MenuButtonType.BakcToMainMenu:
            MainMenuElements menu = FindObjectOfType<MainMenuElements>();
            print("BAAAACK");
            menu.menu.gameObject.SetActive(true);
            menu.credit.gameObject.SetActive(false);
            break;
        }
    }

    public void OnSelect()
    {
        return;
    }
}
