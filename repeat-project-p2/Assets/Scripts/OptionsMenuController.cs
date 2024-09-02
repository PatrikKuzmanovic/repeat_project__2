using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject optionsPanel;
    public Slider volumeSlider;
    public Button backButton;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        backButton.onClick.AddListener(BackToMainMenu);
    }

    public void ShowOptionsMenu()
    {
        optionsPanel.SetActive(true);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void BackToMainMenu()
    {
        optionsPanel.SetActive(false);
    }
}
