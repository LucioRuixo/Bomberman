using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayoutManager layoutManager;

    public Player player;

    public static event Action initialize;

    void Start()
    {
        initialize += InitializePlayerPosition;

        if (initialize != null)
            initialize();
    }

    void InitializePlayerPosition()
    {
        float positionX = layoutManager.firstCellPositionX;
        float positionZ = layoutManager.firstCellPositionZ;

        Vector3 position = new Vector3(positionX, player.positionY, positionZ);

        player.transform.position = position;
    }
}