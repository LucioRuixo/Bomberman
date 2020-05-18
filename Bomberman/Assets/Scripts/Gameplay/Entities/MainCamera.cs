using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    bool onEndgameScreen;

    public Vector3 positionOffset;
    public Vector3 rotation;

    public Transform pivot;
    public Transform canvas;

    void Start()
    {
        onEndgameScreen = false;

        GameManager.gameOver += SetForEndgameScreen;
    }

    void Update()
    {
        if (!onEndgameScreen)
        { 
            transform.position = pivot.position + positionOffset;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    public void SetForEndgameScreen(bool playerWon)
    {
        transform.SetParent(canvas);
        transform.localPosition = new Vector3(0, 0, -600f);
        transform.rotation = Quaternion.identity;

        onEndgameScreen = true;
    }
}
