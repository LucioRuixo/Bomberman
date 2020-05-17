using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float positionY;

    public int enemyAmount;

    public LayoutManager layoutManager;

    public GameObject enemyPrefab;

    void Start()
    {
        positionY = 1f;

        InitializeEnemies();
    }
    
    void InitializeEnemies()
    {
        Vector3 position;

        Quaternion rotation;

        for (int i = 0; i < enemyAmount; i++)
        {
            position = layoutManager.GetRandomPositionInGrid(positionY);

            rotation = Quaternion.Euler(Enemy.GetRandomDirection());

            Instantiate(enemyPrefab, position, rotation, transform);
        }
    }
}