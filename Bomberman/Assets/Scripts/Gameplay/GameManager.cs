using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject layoutManager;
    public GameObject enemyManager;

    public static event Action<bool> gameOver;

    void Start()
    {
        LevelDoor.playerReachedDoor += OnGameOverConditions;
        Player.death += OnGameOverConditions;
    }

    void OnGameOverConditions()
    {
        LevelDoor.playerReachedDoor -= OnGameOverConditions;
        Player.death -= OnGameOverConditions;

        player.SetActive(false);
        layoutManager.SetActive(false);
        enemyManager.SetActive(false);

        if (Player.lives > 0)
        {
            if (gameOver != null)
                gameOver(true);
        }
        else
        {
            if (gameOver != null)
                gameOver(false);
        }
    }
}