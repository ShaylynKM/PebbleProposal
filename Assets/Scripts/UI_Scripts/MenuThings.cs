using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuThings : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnSettingsButton()
    {

    }

    public void OnExitButton()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

}
