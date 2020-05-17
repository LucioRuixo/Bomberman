using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    double explotionTimer;
    double explotionTimerTarget;

    float explotionPositionY;

    public event Action onExplotion;

    void Start()
    {
        explotionTimer = 0d;
        explotionTimerTarget = 3d;

        explotionPositionY = 1f;

        onExplotion += Explode;
    }

    void Update()
    {
        explotionTimer += Time.deltaTime;

        if (explotionTimer >= explotionTimerTarget)
            onExplotion();
    }

    void Explode()
    {
        GameObject explotion = new GameObject("Explotion");
        explotion.transform.position = new Vector3(transform.position.x, explotionPositionY, transform.position.z);
        explotion.AddComponent<Explotion>();

        onExplotion -= Explode;
        Destroy(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            this.gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }
}
