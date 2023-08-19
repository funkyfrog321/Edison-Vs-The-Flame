using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class UIManager : MonoBehaviour
{
    public TMP_Text score;
    public GameObject gameOverMenu;
    public GameManagerScript gameManagerScript;
    

    public void Start()
    {
        score.text = "Stage = " + gameManagerScript.stage;
    }

    public void Update()
    {
        int fixedNumber = gameManagerScript.stage + 1;
        string updatedText = "Stage: " + fixedNumber;
        score.text = updatedText;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += EnableGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
