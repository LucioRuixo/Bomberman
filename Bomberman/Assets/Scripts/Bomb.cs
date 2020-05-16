using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    double explotionTimer;
    double explotionTimerTarget;

    public static float positionY;

    public static event Action<bool> onExplotion;

    void Start()
    {
        explotionTimer = 0d;
        explotionTimerTarget = 3d;

        positionY = 0.75f;
    }

    void Update()
    {
        explotionTimer += Time.deltaTime;

        if (explotionTimer >= explotionTimerTarget)
            Explode();
    }

    void Explode()
    {
        Destroy(this.gameObject);

        if (onExplotion != null)
            onExplotion(false);
    }
}
