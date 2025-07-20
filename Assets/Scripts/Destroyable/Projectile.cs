using UnityEngine;

public class Projectile : MonoBehaviour
{
   
    public int damage = 1;
    public float speed = 15f;
    public float lifetime = 3f;
    
    
    //public GameObject hitEffect;
    public AudioClip hitSound;
    
    private Transform target;
    private Vector3 lastKnownPosition;

    void Start()
    {
       Destroy(gameObject, lifetime);
    }

    public void SetTarget(Transform player)
    {
        target = player;
        lastKnownPosition = target.position;
    }

    void Update()
    {
        // set target position
        Vector3 targetPos = target != null ? target.position : lastKnownPosition;
        
        // direction calculations
        Vector3 moveDirection = (targetPos - transform.position).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;
        
        // facing the right direction
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // if hit player
        if (other.CompareTag("Player"))
        {
            // damage
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // hit effect
            //if (hitEffect != null)
            //{
            //    Instantiate(hitEffect, transform.position, Quaternion.identity);
            //}

            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            }

            // destroy bullet
            Destroy(gameObject);
        }
        
    }
}