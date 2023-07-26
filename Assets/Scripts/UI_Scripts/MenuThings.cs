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

    [SerializeField]
    public GameObject SettingsScreen;
    public GameObject GameOverScreen;
    public AudioClip ButtonClick;
    public AudioClip PlayClick;

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
        GameOverOpen = !GameOverOpen;
        SettingsScreen.SetActive(GameOverScreen);
    }

    public void ToggleGameOver()
    {
        SettingsOpen = !SettingsOpen;
        SettingsScreen.SetActive(SettingsOpen);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    //public void OnSettingsButton()
    //{
    //    if(GameObject.Find("SettingsMenu").activeSelf == true)
    //    {
    //        if(
    //        {
    //            GameObject.Find("SettingsMenu").SetActive(false);
    //        }
    //    }

    //    if (GameObject.Find("SettingsMenu").activeSelf == false)
    //    {
    //        if (Input.GetKeyDown("return"))
    //        {
    //            GameObject.Find("SettingsMenu").SetActive(true);
    //        }
    //    }
    //}

    public void OnExitButton()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

}
