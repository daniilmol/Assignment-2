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
    private int x;
    private int y;
    private Vector3 startingPoint;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        startingPoint = transform.position;
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

    public void Reset(){
        agent.ResetPath();
        agent.isStopped = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        agent.velocity = Vector3.zero;
        agent.Warp(startingPoint);
        //transform.position = startingPoint;
        agent.isStopped = false;
        Patrol();
    }

    public void SetStartingPosition(Vector3 position){
        transform.position = position;
        print(position);
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
