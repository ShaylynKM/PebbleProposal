using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawPlatform : MonoBehaviour
{
    private GameObject _playerCharacter;

    private float _minAngle = 0f;
    private float _maxAngle = 90f;

    void Start()
    {
        // Reference for my player character. Can be deleted and replaced with our actual character later.
        _playerCharacter = GameObject.Find("TestPlayer");
    }

    private void OnTriggerStay2D()
    {
        
    }
}
