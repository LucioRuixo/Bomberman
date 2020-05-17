using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    int gridSideLenght;
    int columnsPerSide;
    int destroyableColumnsAmount;

    float columnSize;
    float columnPositionY;

    List<Vector2> occupiedGridPositions;

    List<GameObject> columns;
    List<GameObject> destroyableColumns;

    [HideInInspector] public float firstCellPositionX;
    [HideInInspector] public float firstCellPositionZ;

    public GameManager gameManager;

    public GameObject columnPrefab;
    public GameObject destroyableColumnPrefab;

    public Transform columnParent;
    public Transform destroyableColumnParent;

    void Awake()
    {
        gridSideLenght = 23;
        columnsPerSide = gridSideLenght / 2;
        destroyableColumnsAmount = 100;

        columnSize = 1f;
        columnPositionY = 1f;

        occupiedGridPositions = new List<Vector2>();

        columns = new List<GameObject>();
        destroyableColumns = new List<GameObject>();

        firstCellPositionX = -11f;
        firstCellPositionZ = 11f;

        GameManager.initialize += InitializeColumns;
        GameManager.initialize += InitializeDestroyableColumns;
    }

    void InitializeColumns()
    {
        float leftMostColumnX = -10f;
        float currentRowZ = 10f;

        Vector3 currentColumnPosition;

        currentColumnPosition = new Vector3(leftMostColumnX, columnPositionY, currentRowZ);

        while (columns.Count < columnsPerSide * columnsPerSide)
        {
            columns.Add(Instantiate(columnPrefab, currentColumnPosition, Quaternion.identity, columnParent));

            if (columns.Count % columnsPerSide != 0)
                currentColumnPosition.x += columnSize * 2;
            else
            {
                currentColumnPosition.x = leftMostColumnX;
                currentColumnPosition.z -= columnSize * 2;
            }
        }
    }

    void InitializeDestroyableColumns()
    {
        int maxColumnAmount = gridSideLenght * gridSideLenght - columnsPerSide * columnsPerSide;

        Vector3 currentColumnPosition;

        while (destroyableColumns.Count < destroyableColumnsAmount && destroyableColumns.Count < maxColumnAmount)
        {
            currentColumnPosition = GetRandomPositionInGrid(columnPositionY);

            destroyableColumns.Add(Instantiate(destroyableColumnPrefab, currentColumnPosition, Quaternion.identity, destroyableColumnParent));
        }
    }

    public Vector3 GetRandomPositionInGrid(float positionY)
    {
        bool overColumn;
        bool occupied = false;
        bool insidePlayerSafeArea;

        float cellSize = 1f;
        float positionX;
        float positionZ;

        Vector2 gridPosition;

        Vector3 position;

        do
        {
            gridPosition.x = UnityEngine.Random.Range(0, gridSideLenght - 1);
            gridPosition.y = UnityEngine.Random.Range(0, gridSideLenght - 1);

            if (gridPosition.x % 2 != 0 && gridPosition.y % 2 != 0)
                overColumn = true;
            else
                overColumn = false;

            if (occupiedGridPositions.Count > 0)
            {
                foreach (Vector2 occupiedGridPosition in occupiedGridPositions)
                {
                    if (gridPosition == occupiedGridPosition)
                    {
                        occupied = true;
                        break;
                    }
                    else
                        occupied = false;
                }
            }

            if ((gridPosition.y == 0 && (gridPosition.x >= 0 || gridPosition.x <= 2))
                ||
                (gridPosition.x == 0 && (gridPosition.y == 1 || gridPosition.y == 2)))
                insidePlayerSafeArea = true;
            else
                insidePlayerSafeArea = false;
        }
        while (overColumn || occupied || insidePlayerSafeArea);

        occupiedGridPositions.Add(gridPosition);

        positionX = firstCellPositionX + cellSize * gridPosition.x;
        positionZ = firstCellPositionZ - cellSize * gridPosition.y;
        position = new Vector3(positionX, positionY, positionZ);

        return position;
    }
}