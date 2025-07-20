using UnityEngine;

public class RammingEnemy : MonoBehaviour
{
    //damage 
    public int damageToPlayer = 1;  
    public bool destroyOnCollision = true;

    public GameObject enemy;

    //effect
    public GameObject explosionEffect;  
    public AudioClip collisionSound;    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            // inflicts da,age to player
            // PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(damageToPlayer);
            // }

            // effect
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            if (collisionSound != null)
            {
                AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            }

            // destroy after collision
            if (destroyOnCollision)
            {
                Destroy(enemy);
            }
        }
    }
}