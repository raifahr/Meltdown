using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint; 
    public float fireRate = 1f;
    public float projectileSpeed = 15f;
    public float attackRange = 10f;
    
    
    public AudioClip shootSound;
    
    private Transform player;
    private float nextFireTime;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        
        // distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // facing player
        Vector3 lookDirection = (player.position - transform.position).normalized;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection);
        
        // check shoot condition
        if (distanceToPlayer <= attackRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        
        // create bullet
        GameObject projectile = Instantiate(
            projectilePrefab, 
            firePoint.position, 
            firePoint.rotation
        );
        
        // bullet's setting
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(player);
            projectileScript.speed = projectileSpeed;
        }
        
        // sount
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
    }

    // range of shooting
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}