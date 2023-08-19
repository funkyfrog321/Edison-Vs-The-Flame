using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public TMP_Text score;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;
    public GameManagerScript gameManagerScript;
    public TMP_Text gameover_enemyRecord;
    public TMP_Text victory_enemyRecord;

    public void Start()
    {
        score.text = "Stage = " + gameManagerScript.stage;
        gameover_enemyRecord.text = "Enemies Killed: " + gameManagerScript.num_enemies_killed;
        victory_enemyRecord.text = "Enemies Killed: " + gameManagerScript.num_enemies_killed;
    }

    public void Update()
    {
        int fixedNumber = gameManagerScript.stage + 1;
        string updatedText = "Stage: " + fixedNumber;
        score.text = updatedText;

        gameover_enemyRecord.text = "Enemies Killed: " + gameManagerScript.num_enemies_killed;
        victory_enemyRecord.text = "Enemies Killed: " + gameManagerScript.num_enemies_killed;
    }

    private void OnEnable()
    {
            PlayerHealth.OnPlayerDeath += EnableGameOverMenu;
            GameManagerScript.OnChandelureKilled += EnableVictoryMenu;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= EnableGameOverMenu;
        GameManagerScript.OnChandelureKilled -= EnableVictoryMenu;
    }

    public void EnableVictoryMenu()
    {
        victoryMenu.SetActive(true);
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
