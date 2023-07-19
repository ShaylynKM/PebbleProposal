using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Used to check from other scripts if the game is paused
    public static bool GamePaused = false;

    public GameObject PauseUI;

    private void Start()
    {
        GamePaused = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Sets the pause menu to inactive
        PauseUI.SetActive(false);

        // Resumes time
        Time.timeScale = 1f;

        // Flags the game as unpaused
        GamePaused = false;
    }

    void Pause()
    {
        // Sets the pause menu to active
        PauseUI.SetActive(true);

        // Stops time
        Time.timeScale = 0f;

        // Flags the game as paused
        GamePaused = true;
    }

    public void LoadSettings()
    {

    }

    public void Quit()
    {
        // Returns to main menu. WE WILL NEED SAVE STATES.
        SceneManager.LoadScene("main");
    }
}
