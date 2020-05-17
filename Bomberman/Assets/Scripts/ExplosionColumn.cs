using UnityEngine;

public class ExplosionColumn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Explosion>().playerInsideExplosionArea = true;
        }
    }
}