using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private bool _settingsOpen = false;

    [SerializeField]
    private GameObject _settingsScreen;

    private void ToggleSettings()
    {
        _settingsOpen = !_settingsOpen;
        _settingsScreen.SetActive(_settingsOpen);
    }

    //public void OnSettingsButton()
    //{

    //    if (GameObject.Find("SettingsMenu").activeSelf == true)
    //    {
    //        //if ()
    //        //{
    //        //    GameObject.Find("SettingsMenu").SetActive(false);
    //        //}
    //    }

    //    if (GameObject.Find("SettingsMenu").activeSelf == false)
    //    {
    //        if (Input.GetKeyDown("return"))
    //        {
    //            GameObject.Find("SettingsMenu").SetActive(true);
    //        }
    //    }
    //}
}
