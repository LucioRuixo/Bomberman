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

    List<GameObject> columns;
    List<GameObject> destroyableColumns;

    public GameManager gameManager;

    public GameObject columnPrefab;
    public GameObject destroyableColumnPrefab;

    public Transform columnParent;
    public Transform destroyableColumnParent;

    void Awake()
    {
        gridSideLenght = gameManager.gridSideLenght;
        columnsPerSide = gridSideLenght / 2;
        destroyableColumnsAmount = 100;

        columnSize = 1f;
        columnPositionY = 1f;

        columns = new List<GameObject>();
        destroyableColumns = new List<GameObject>();

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
            currentColumnPosition = gameManager.GetRandomPositionInGrid(columnPositionY);

            destroyableColumns.Add(Instantiate(destroyableColumnPrefab, currentColumnPosition, Quaternion.identity, destroyableColumnParent));
        }
    }
}