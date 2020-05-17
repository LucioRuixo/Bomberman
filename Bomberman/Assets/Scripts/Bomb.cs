using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    double explosionTimer;
    double explosionTimerTarget;

    float explosionPositionY;

    public event Action onExplosion;

    void Start()
    {
        explosionTimer = 0d;
        explosionTimerTarget = 2d;

        explosionPositionY = 1f;

        onExplosion += Explode;
    }

    void Update()
    {
        explosionTimer += Time.deltaTime;

        if (explosionTimer >= explosionTimerTarget)
            onExplosion();
    }

    void Explode()
    {
        GameObject explosion = new GameObject("Explosion");
        explosion.transform.position = new Vector3(transform.position.x, explosionPositionY, transform.position.z);
        explosion.AddComponent<Explosion>();

        onExplosion -= Explode;
        Destroy(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            this.gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }
}
