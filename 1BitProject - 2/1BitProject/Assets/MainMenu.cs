using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void Awake()
    {
        PlayerPrefs.SetFloat("volume", 0.8f);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
    }
}
