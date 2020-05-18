using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool playerWon;

    public Player player;

    void Start()
    {
        LevelDoor.playerReachedDoor += GameOver;
    }

    void GameOver()
    {
        if (player.lives > 0)
            playerWon = true;
        else
            playerWon = false;

        Debug.Log("Game over, " + playerWon);
    }
}