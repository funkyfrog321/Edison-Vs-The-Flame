using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSingleton : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    //AudioMixer audioMixer = new AudioMixer();

    public AudioSource edisonMusic;
    public AudioSource edisonStartup;
    public AudioSource edisonShoot;
    public AudioSource edisonKill;
    public AudioSource edisonGameover;
    public AudioSource edisonOof;

    public static AudioSingleton Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayGameMusic()
    {
        edisonMusic.Play();
    }
    public void StopGameMusic()
    {
        edisonMusic.Stop();
    }

    public void PlayEnemyKilled()
    {
        edisonKill.Play();
    }

    public void PlayShoot()
    {
        edisonShoot.Play();
    }

    public void PlayStartup()
    {
        edisonStartup.Play();
    }

    public void PlayOof()
    {
        edisonOof.Play();
    }

    public void PlayGameOver()
    {
        edisonGameover.Play();
    }
}
