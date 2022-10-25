using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;

    void Start()    
    {
        agent = GetComponent<NavMeshAgent>();
        Patrol();
    }

    void Patrol() {
        int x = Random.Range(0, MazeGenerator.cells.GetLength(0));
        int y = Random.Range(0, MazeGenerator.cells.GetLength(0));
        destination = MazeGenerator.cells[x, y].transform.position;
        agent.destination = destination;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, destination) < 1){
            Patrol();
        }
    }
}
