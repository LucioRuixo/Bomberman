using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    double explosionTimer;
    double explosionTimerTarget;

    float explosionPositionY;

    public event Action explosion;

    void Start()
    {
        explosionTimer = 0d;
        explosionTimerTarget = 2d;

        explosionPositionY = 1f;

        explosion += Explode;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            this.gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }

    void Update()
    {
        explosionTimer += Time.deltaTime;

        if (explosionTimer >= explosionTimerTarget)
            explosion();
    }

    void Explode()
    {
        GameObject newExplosion = new GameObject("Explosion");
        newExplosion.transform.position = new Vector3(transform.position.x, explosionPositionY, transform.position.z);
        newExplosion.AddComponent<Explosion>();

        explosion -= Explode;
        Destroy(this.gameObject);
    }
}
