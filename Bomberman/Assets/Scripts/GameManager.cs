using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public int gridSideLenght;

    List<Vector2> occupiedGridPositions;

    public Player player;

    public static event Action initialize;

    void Awake()
    {
        gridSideLenght = 23;

        occupiedGridPositions = new List<Vector2>();
    }

    void Start()
    {
        initialize += InitializePlayerPosition;

        if (initialize != null)
            initialize();
    }

    void InitializePlayerPosition()
    {
        player.transform.position = GetRandomPositionInGrid(player.positionY);
    }

    public Vector3 GetRandomPositionInGrid(float positionY)
    {
        bool overColumn;
        bool positionOccupied = false;

        float cellSize = 1f;
        float firstColumnPositionXZ = -11f;
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

        positionX = firstColumnPositionXZ + cellSize * gridPosition.x;
        positionZ = firstColumnPositionXZ + cellSize * gridPosition.y;
        position = new Vector3(positionX, positionY, positionZ);

        return position;
    }
}