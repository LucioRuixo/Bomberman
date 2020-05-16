using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    int gridSideLenght;
    int columnsPerSide;

    float columnSize;
    float columnY;

    Vector3 currentColumnPosition;

    List<GameObject> columns = new List<GameObject>();
    List<GameObject> destroyableColumns = new List<GameObject>();

    public int destroyableColumnsAmount;

    public GameObject columnPrefab;
    public GameObject destroyableColumnPrefab;

    public Transform columnParent;
    public Transform destroyableColumnParent;

    void Start()
    {
        gridSideLenght = 23;
        columnsPerSide = gridSideLenght / 2;

        columnSize = 1f;
        columnY = 1f;

        InitiateColumns();
        InitiateDestroyableColumns();
    }

    void InitiateColumns()
    {
        float leftMostColumnX = -10f;
        float currentRowZ = 10f;

        currentColumnPosition = new Vector3(leftMostColumnX, columnY, currentRowZ);

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

    void InitiateDestroyableColumns()
    {
        float firstColumnPositionXZ = -11f;

        bool overColumn;
        bool positionOccupied = false;

        Vector2 gridPosition;

        List<Vector2> occupiedGridPositions = new List<Vector2>();

        while (destroyableColumns.Count < destroyableColumnsAmount)
        {
            do
            {
                gridPosition.x = Random.Range(0, gridSideLenght - 1);
                gridPosition.y = Random.Range(0, gridSideLenght - 1);

                if (gridPosition.x % 2 != 0 && gridPosition.y % 2 != 0)
                    overColumn = true;
                else
                    overColumn = false;

                if (occupiedGridPositions.Count > 0)
                {
                    foreach (Vector2 position in occupiedGridPositions)
                    {
                        if (gridPosition == position)
                        {
                            positionOccupied = true;
                            break;
                        }
                        else
                            positionOccupied = false;
                    }
                }
            }
            while (overColumn || positionOccupied);

            occupiedGridPositions.Add(gridPosition);

            currentColumnPosition.x = firstColumnPositionXZ + columnSize * gridPosition.x;
            currentColumnPosition.z = firstColumnPositionXZ + columnSize * gridPosition.y;

            destroyableColumns.Add(Instantiate(destroyableColumnPrefab, currentColumnPosition, Quaternion.identity, destroyableColumnParent));
        }
    }
}