using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject game;
    public GameObject startmenu;


    public void PlayButton()
    {
        // Play Now Button has been pressed
        game.SetActive(true);
        startmenu.SetActive(false);
    }

    public void OptionsButton()
    {
        // Show Options Menu
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}