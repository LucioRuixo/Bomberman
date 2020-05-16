using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    public GameObject columnPrefab;

    public Transform columnParent;

    List<GameObject> columns = new List<GameObject>();

    void Start()
    {
        InitiateColumns();
    }

    void InitiateColumns()
    {
        int columnsPerSide = 11;

        float columnSize = 1;
        float leftMostColumnX = -10f;
        float columnY = 1f;
        float currentRowZ = 10f;

        Vector3 currentColumnPosition = new Vector3(leftMostColumnX, columnY, currentRowZ);

        while (columns.Count < columnsPerSide * columnsPerSide)
        {
            if (columns.Count == 0)
            {
                columns.Add(Instantiate(columnPrefab, currentColumnPosition, Quaternion.identity, columnParent));
                currentColumnPosition.x += columnSize * 2;
            }
            else
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
    }
}
