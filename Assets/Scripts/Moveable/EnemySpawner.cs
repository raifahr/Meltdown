using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;  
    public float spawnInterval = 5f; 
    public int maxEnemies = 10;     

    private float timer;
    private int currentEnemies = 0;

    void Update()
    {

        timer += Time.deltaTime;


        if (timer >= spawnInterval && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
        
    }

    void SpawnEnemy()
    {
        
        Vector3 randomPoint = GetRandomNavMeshPoint();
        GameObject enemyProduced = Instantiate(enemy, randomPoint, Quaternion.identity);
        
        currentEnemies++;
        Debug.Log($"Enemy Spawned: {currentEnemies}");
    }

    
    Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10f;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas);
        return hit.position;
    }


    public void OnEnemyDeath()
    {
        currentEnemies--;
    }
}