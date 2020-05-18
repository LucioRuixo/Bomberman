using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    public GameObject gameplayUI;
    public GameObject endgameScreen;
    public GameObject lives;
    public GameObject endgameText;

    void Start()
    {
        lives.GetComponent<TextMeshProUGUI>().text = "Lives: " + Player.lives;

        Player.damageReceived += UpdateLives;

        GameManager.gameOver += OnGameOver;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void UpdateLives()
    {
        lives.GetComponent<TextMeshProUGUI>().text = "Lives: " + Player.lives;
    }

    public void OnGameOver(bool playerWon)
    {
        gameplayUI.SetActive(false);
        endgameScreen.SetActive(true);

        if (playerWon)
            endgameText.GetComponent<TextMeshProUGUI>().text = "You won!";
        else
            endgameText.GetComponent<TextMeshProUGUI>().text = "You lost :(";
    }
}
