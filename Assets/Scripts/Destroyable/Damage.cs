using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damageAmount = 20.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                Vector3 hitDirection = other.transform.position - transform.position;
                health.TakeDamage(damageAmount);
            }
        }
    }
}
