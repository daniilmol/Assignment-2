using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    /**
    * Enemy's AI 
    */
    private NavMeshAgent agent;
    /**
    * Destination cell for patrolling
    */
    private Vector3 destination;

    /**
    * Initializes AI and begin patrolling
    */
    void Start()    
    {
        agent = GetComponent<NavMeshAgent>();
        Patrol();
    }

    /**
    * Chooses a random cell and sets the AI's destination to its position
    */
    void Patrol() {
        int x = Random.Range(0, MazeGenerator.cells.GetLength(0));
        int y = Random.Range(0, MazeGenerator.cells.GetLength(0));
        destination = MazeGenerator.cells[x, y].transform.position;
        agent.destination = destination;
    }

    /**
    * Checks if the AI reached its destination, if so, call Patrol()
    */
    void Update()
    {
        if(Vector3.Distance(transform.position, destination) < 1){
            Patrol();
        }
    }
}
