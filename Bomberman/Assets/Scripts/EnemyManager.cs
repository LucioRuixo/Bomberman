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

        for (int i = 0; i < enemyAmount; i++)
        {
            position = layoutManager.GetRandomPositionInGrid(positionY);

            Instantiate(enemyPrefab, position, Quaternion.identity, transform);
        }
    }
}