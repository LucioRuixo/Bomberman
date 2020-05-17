using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    bool damageDealtToPlayer;

    double explosionTimer;
    double explosionTimerTarget;

    LayoutManager layoutManager;

    Player player;

    GameObject explosionColumnPrefab;

    [HideInInspector] public bool playerInsideExplosionArea;

    public static event Action onDamageDealtToPlayer;

    void Start()
    {
        damageDealtToPlayer = false;

        explosionTimer = 0d;
        explosionTimerTarget = 1d;

        layoutManager = GameObject.Find("Layout Manager").GetComponent<LayoutManager>();

        player = GameObject.Find("Player").GetComponent<Player>();

        playerInsideExplosionArea = false;

        InitializeExplosionColumns();
    }

    void Update()
    {
        explosionTimer += Time.deltaTime;

        if (explosionTimer >= explosionTimerTarget)
            Destroy(this.gameObject);

        if (playerInsideExplosionArea && !damageDealtToPlayer)
        {
            onDamageDealtToPlayer();
            damageDealtToPlayer = true;
        }
    }

    void InitializeExplosionColumns()
    {
        bool leftColumnLineNotCompleted = true;
        bool rightColumnLineNotCompleted = true;
        bool upColumnLineNotCompleted = true;
        bool downColumnLineNotCompleted = true;

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

            if (leftColumnLineNotCompleted)
                leftColumnLineNotCompleted = InitializeOuterColumn(left);
            else
                left = Vector2Int.zero;

            if (rightColumnLineNotCompleted)
                rightColumnLineNotCompleted = InitializeOuterColumn(right);
            else
                right = Vector2Int.zero;

            if (upColumnLineNotCompleted)
                upColumnLineNotCompleted = InitializeOuterColumn(up);
            else
                up = Vector2Int.zero;

            if (downColumnLineNotCompleted)
                downColumnLineNotCompleted = InitializeOuterColumn(down);
            else
                down = Vector2Int.zero;

            layoutManager.CheckColumnExplosion(left, right, up, down);
        }
    }

    bool InitializeOuterColumn(Vector2Int direction)
    {
        Vector3 position;
        
        if (direction.x != transform.position.x)
        {
            if (direction.x < 0 || direction.x > layoutManager.gridSideLenght - 1)
                return false;
        }
        else
        {
            if (-direction.y < 0 || -direction.y > layoutManager.gridSideLenght - 1)
                return false;
        }

        if (layoutManager.grid[direction.x, -direction.y] == LayoutManager.cellStates.Column)
            return false;
        else
        {
            position = new Vector3(direction.x, layoutManager.columnPositionY, direction.y);

            Instantiate(layoutManager.explosionColumnPrefab, position, Quaternion.identity, transform);

            return true;
        }
    }
}