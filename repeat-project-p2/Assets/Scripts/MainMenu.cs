using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject trackSelectionText;
    public GameObject track1Button;
    public GameObject track2Button;

    private void Start()
    {
        ShowMainMenu();
    }
    public void PlayGame()
    {
        playButton.SetActive(false);
        optionsButton.SetActive(false);

        trackSelectionText.SetActive(true);
        track1Button.SetActive(true);
        track2Button.SetActive(true);
    }

    public void LoadTrack1()
    {
        SceneManager.LoadScene("Track_1Scene");
    }

    public void LoadTrack2()
    {
        SceneManager.LoadScene("Track_2Scene");
    }

    public void OpenOptions()
    {
        Debug.Log("Options button clicked");
    }

    private void ShowMainMenu()
    {
        playButton.SetActive(true);
        optionsButton.SetActive(true);
        trackSelectionText.SetActive(false);
        track1Button.SetActive(false);
        track2Button.SetActive(false);
    }
}
