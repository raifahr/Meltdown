using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public PowerManager powerManager;
    public Rigidbody playerRigidbody;
    public float knockbackForce = 5f;
    public float damageCooldown = 0.25f;

    private float lastDamageTime = -999f;

    void Start()
    {
        if (playerRigidbody == null)
            playerRigidbody = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (IsEnemyOrHazard(collision.gameObject))
        {
            Vector3 knockbackDir = (transform.position - collision.transform.position).normalized;
            HandleHit(knockbackDir);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsEnemyOrHazard(other.gameObject))
        {
            Vector3 knockbackDir = (transform.position - other.transform.position).normalized;
            HandleHit(knockbackDir);
        }
    }

    private void HandleHit(Vector3 knockbackDirection)
    {
        if (Time.time - lastDamageTime < damageCooldown) return;

        TakeDamage();

        if (playerRigidbody != null)
        {
            playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }

        lastDamageTime = Time.time;
    }

    private bool IsEnemyOrHazard(GameObject obj)
    {
        return obj.CompareTag("Enemy") || obj.CompareTag("Hazard");
    }

    void TakeDamage()
    {
        if (powerManager != null)
        {
            powerManager.ModifyHealth(-1);
            Debug.Log("Player took damage!");
        }
    }
}
