using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination = new Vector3(-10f, -10f, -10f);
    private bool initialized;
    // Start is called before the first frame update
    void Start()    
    {
        initialized = true;
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        int x = Random.Range(0, MazeGenerator.cells.GetLength(0));
        int y = Random.Range(0, MazeGenerator.cells.GetLength(0));
        destination = new Vector3(MazeGenerator.cells[x, y].gameObject.transform.position.x, 0, MazeGenerator.cells[x, y].gameObject.transform.position.z);
        agent.destination = destination;
        agent.isStopped = false;
        Patrol();
    }

    void Patrol() {
        if (agent.isStopped && agent.destination == destination && !initialized) {
            print("ENEMY PATROLLING");
            int x = Random.Range(0, MazeGenerator.cells.GetLength(0));
            int y = Random.Range(0, MazeGenerator.cells.GetLength(0));
            agent.isStopped = false;
            destination = new Vector3(MazeGenerator.cells[x, y].gameObject.transform.position.x, 0, MazeGenerator.cells[x, y].gameObject.transform.position.z);
            agent.destination = destination;
            initialized = false;
        }
        if (agent.destination == destination && !initialized) {
            agent.isStopped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
}
