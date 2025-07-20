using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.transform.position); // move towards the player
        }
    }
}