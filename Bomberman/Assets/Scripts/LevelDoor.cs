using System;
using UnityEngine;

public class LevelDoor : MonoBehaviour
{
    public static event Action playerReachedDoor;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && EnemyManager.enemyAmount <= 0)
            playerReachedDoor();
    }
}
