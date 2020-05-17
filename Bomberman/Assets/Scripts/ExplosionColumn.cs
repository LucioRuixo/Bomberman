using UnityEngine;

public class ExplosionColumn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Explosion>().shouldDealDamageToPlayer = true;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Explosion.onDamageDealtToEnemy += other.GetComponent<Enemy>().OnDamageReceived;

            GetComponentInParent<Explosion>().shouldDealDamageToEnemy = true;
        }
    }
}