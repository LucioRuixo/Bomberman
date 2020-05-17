using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    double explosionTimer;
    double explosionTimerTarget;

    LayoutManager layoutManager;

    Player player;

    GameObject explosionColumnPrefab;

    void Start()
    {
        explosionTimer = 0d;
        explosionTimerTarget = 1d;

        layoutManager = GameObject.Find("Layout Manager").GetComponent<LayoutManager>();

        player = GameObject.Find("Player").GetComponent<Player>();

        InitializeExplosionColumns();
    }

    void Update()
    {
        explosionTimer += Time.deltaTime;

        if (explosionTimer >= explosionTimerTarget)
            Destroy(this.gameObject);
    }

    void InitializeExplosionColumns()
    {
        Vector2Int left;
        Vector2Int right;
        Vector2Int up;
        Vector2Int down;


        Instantiate(layoutManager.explosionColumnPrefab, transform.position, Quaternion.identity, transform);

        for (int i = 1; i <= player.bombRange; i++)
        {
            left = new Vector2Int((int)transform.position.x - i, (int)transform.position.z);
            right = new Vector2Int((int)transform.position.x + i, (int)transform.position.z);
            up = new Vector2Int((int)transform.position.x, (int)transform.position.z + i);
            down = new Vector2Int((int)transform.position.x, (int)transform.position.z - i);

            InitializeOuterColumns(left);
            InitializeOuterColumns(right);
            InitializeOuterColumns(up);
            InitializeOuterColumns(down);
        }
    }

    void InitializeOuterColumns(Vector2Int direction)
    {
        Vector3 position;
        
        if (direction.x != transform.position.x)
        {
            if (direction.x >= 0 && direction.x <= layoutManager.gridSideLenght - 1)
            {
                if (layoutManager.grid[direction.x, -direction.y] != LayoutManager.cellStates.Column)
                {
                    position = new Vector3(direction.x, layoutManager.columnPositionY, direction.y);

                    Instantiate(layoutManager.explosionColumnPrefab, position, Quaternion.identity, transform);

                    layoutManager.CheckColumnExplosion(new Vector3(direction.x, layoutManager.columnPositionY, direction.y));
                }
            }
        }
        else
        {
            if (-direction.y >= 0 && -direction.y <= layoutManager.gridSideLenght - 1)
            {
                if (layoutManager.grid[direction.x, -direction.y] != LayoutManager.cellStates.Column)
                {
                    position = new Vector3(direction.x, layoutManager.columnPositionY, direction.y);

                    Instantiate(layoutManager.explosionColumnPrefab, position, Quaternion.identity, transform);

                    layoutManager.CheckColumnExplosion(new Vector3(direction.x, layoutManager.columnPositionY, direction.y));
                }
            }
        }
    }
}