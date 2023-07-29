using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// In generalf or the UI, I think it is cute but I would very much like to see more levels with some sort of UI screen show up to say you beat a level. You will also need to look at how to persist what you have gained from one level to the next. What you are looking for is persistence, which will allow for information to be stored. Here is a good tutorial for this: https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#
/// </summary>
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
