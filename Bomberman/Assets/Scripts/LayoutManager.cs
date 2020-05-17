using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    public enum cellStates
    {
        Empty,
        Column,
        DestroyableColumn,
        ExplotionColumn
    }

    int columnsPerSide;
    int destroyableColumnsAmount;

    [HideInInspector] public int gridSideLenght;

    [HideInInspector] public float columnPositionY;

    [HideInInspector] public cellStates[,] grid;

    public GameManager gameManager;

    public GameObject columnPrefab;
    public GameObject destroyableColumnPrefab;
    public GameObject explotionColumnPrefab;

    public Transform columnParent;
    public Transform destroyableColumnParent;

    void Start()
    {
        columnsPerSide = gridSideLenght / 2;
        destroyableColumnsAmount = 100;

        gridSideLenght = 23;

        columnPositionY = 1f;

        grid = new cellStates[gridSideLenght, gridSideLenght];

        InitializeColumns();
        InitializeDestroyableColumns();
    }

    void InitializeColumns()
    {
        Vector3 currentColumnPosition;

        for (int y = 0; y < gridSideLenght; y++)
        {
            for (int x = 0; x < gridSideLenght; x++)
            {
                if (y % 2 != 0 && x % 2 != 0)
                {
                    grid[x, y] = cellStates.Column;

                    currentColumnPosition = new Vector3(x, columnPositionY, -y);
                    Instantiate(columnPrefab, currentColumnPosition, Quaternion.identity, columnParent);
                }
            }
        }
    }

    void InitializeDestroyableColumns()
    {
        int maxColumnAmount = gridSideLenght * gridSideLenght - columnsPerSide * columnsPerSide;

        Vector3 currentColumnPosition;

        Mathf.Clamp(destroyableColumnsAmount, 0, maxColumnAmount);

        for (int i = 0; i < destroyableColumnsAmount; i++)
        {
            currentColumnPosition = GetRandomPositionInGrid(columnPositionY);

            Instantiate(destroyableColumnPrefab, currentColumnPosition, Quaternion.identity, destroyableColumnParent);
        }
    }

    public Vector3 GetRandomPositionInGrid(float positionY)
    {
        bool occupied;
        bool insidePlayerSafeArea;

        float positionX;
        float positionZ;

        Vector2Int gridPosition = Vector2Int.zero;

        Vector3 position;

        do
        {
            gridPosition.x = UnityEngine.Random.Range(0, gridSideLenght - 1);
            gridPosition.y = UnityEngine.Random.Range(0, gridSideLenght - 1);

            if (grid[gridPosition.x, gridPosition.y] != cellStates.Empty)
                occupied = true;
            else
                occupied = false;

            if ((gridPosition.y == 0 && (gridPosition.x >= 0 || gridPosition.x <= 2))
                ||
                (gridPosition.x == 0 && (gridPosition.y == 1 || gridPosition.y == 2)))
                insidePlayerSafeArea = true;
            else
                insidePlayerSafeArea = false;
        }
        while (occupied || insidePlayerSafeArea);

        grid[gridPosition.x, gridPosition.y] = cellStates.DestroyableColumn;

        positionX = gridPosition.x;
        positionZ = -gridPosition.y;
        position = new Vector3(positionX, positionY, positionZ);

        return position;
    }

    public void DestroyDestroyableColumn()
    {

    }
}