using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public AudioSource edisonStartup;
    public AudioSource edisonShoot;
    public AudioSource edisonKill;
    public AudioSource edisonGameover;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void PlayGameOver()
    {
        edisonGameover.Play();
    }
}
