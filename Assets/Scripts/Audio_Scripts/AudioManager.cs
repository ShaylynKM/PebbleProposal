using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioSource jumpSound;
    public AudioSource backgroundSound;

    [SerializeField] AudioClip buttonClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip backgroundClip;

    public void OnButtonClick()
    {
        buttonSound.clip = buttonClip;
        buttonSound.Play();
    }

    public void OnJump()
    {
        jumpSound.clip = jumpClip;
        jumpSound.Play();
    }

    public void OnPlayingBG()
    {
        backgroundSound.clip = backgroundClip;
        backgroundSound.Play();
    }
}
