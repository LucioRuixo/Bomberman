using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float positionY;

    [HideInInspector] public static int enemyAmount;
    public int enemies;

    public LayoutManager layoutManager;

    public GameObject enemyPrefab;

    void Start()
    {
        enemyAmount = 0;

        positionY = 1f;

        InitializeEnemies();

        Explosion.damageDealtToEnemy += OnEnemyDeath;
    }

    void InitializeEnemies()
    {
        Vector3 position;

        Quaternion rotation;

        for (int i = 0; i < enemies; i++)
        {
            position = layoutManager.GetRandomPositionInGrid(positionY);

            rotation = Quaternion.Euler(Enemy.GetRandomDirection());

            Instantiate(enemyPrefab, position, rotation, transform);

            enemyAmount++;
        }
    }

    void OnEnemyDeath()
    {
        enemyAmount--;
    }
}