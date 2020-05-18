using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool playerWon;

    void Start()
    {
        LevelDoor.playerReachedDoor += GameOver;
        Player.death += GameOver;
    }

    void GameOver()
    {
        LevelDoor.playerReachedDoor -= GameOver;
        Player.death -= GameOver;

        if (Player.lives > 0)
            playerWon = true;
        else
            playerWon = false;

        Debug.Log("Game over, " + playerWon);
    }
}