using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuThings : MonoBehaviour
{
    public bool SettingsOpen = false;
    public bool GameOverOpen = false;
    public bool PauseOpen = false;

    [SerializeField]
    public GameObject SettingsScreen;
    public GameObject PauseScreen;
    public GameObject GameOverScreen;
    public AudioClip ButtonClick;
    public AudioClip PlayClick;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("main");
    }

    public void ToggleSettings()
    {
        SettingsOpen = !SettingsOpen;
        SettingsScreen.SetActive(SettingsOpen);
    }

    public void TogglePause()
    {
        PauseOpen = !PauseOpen;
        PauseScreen.SetActive(PauseOpen);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Level1");
    }


    public void OnExitButton()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

}
