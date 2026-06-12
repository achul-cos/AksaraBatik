using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu_UI_Helpers : MonoBehaviour
{
    // Title Scene
    public TextMeshProUGUI sceneTitle;

    // Button Play
    public Button playButton;

    // Button Quit
    public Button quitButton;

    public void Start()
    {
        // Mengisi scene title dengan game state yang sedang berjalan
        sceneTitle.text = GameManager.Instance.SceneName;

        // Subscribe trigger event pressing button playButton
        playButton.onClick.AddListener(RunPlayButton);

        // Subscribe trigger event pressing button quitButton
        quitButton.onClick.AddListener(RunQuitButton);
    }

    // Fungsi ketika tombol play ditekan
    public void RunPlayButton()
    {
        GameManager.Instance.Play();
    }

    public void RunQuitButton()
    {
        GameManager.Instance.Quit();
    }
}
