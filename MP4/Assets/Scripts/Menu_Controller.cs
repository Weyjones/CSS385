using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Controller : MonoBehaviour {

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject backButton;

    [SerializeField] private Text gameTitle;
    [SerializeField] private Text gameTeam;

    [SerializeField] private Text creditsTitle;
    [SerializeField] private Text creditsText;

    void Start()
    {
        SwitchToMainMenu();
    }



    // Show the credits for our game
    public void SwitchToCredits()
    {
        startButton.SetActive(false);
        quitButton.SetActive(false);
        creditsButton.SetActive(false);

        backButton.SetActive(true);

        gameTeam.enabled = false;
        gameTitle.enabled = false;

        
        creditsText.enabled = true;
        creditsTitle.enabled = true;
    }

    // Show the main menu
    public void SwitchToMainMenu()
    {
        startButton.SetActive(true);
        quitButton.SetActive(true);
        creditsButton.SetActive(true);

        backButton.SetActive(false);

        gameTeam.enabled = true;
        gameTitle.enabled = true;

        creditsText.enabled = false;
        creditsTitle.enabled = false;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
