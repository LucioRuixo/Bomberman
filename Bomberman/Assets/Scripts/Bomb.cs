using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    double explotionTimer;
    double explotionTimerTarget;

    public static event Action<bool> onExplotion;

    void Start()
    {
        explotionTimer = 0d;
        explotionTimerTarget = 3d;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            this.gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }
}
