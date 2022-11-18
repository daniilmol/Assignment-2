using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private Vector3 startingPoint;
    private float health;
    private MazeGenerator mazeGenerator;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip spawnSound;
    private float x;
    private float y;
    void Start(){
        mazeGenerator = GameObject.Find("MazeGenerator").GetComponent<MazeGenerator>();
        health = 3;
        agent = GetComponent<NavMeshAgent>();
        startingPoint = transform.position;
        GetComponent<AudioSource>().PlayOneShot(spawnSound);
        Patrol();
    }
    private void Patrol(){
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

    public void Reset(){
        agent.ResetPath();
        agent.isStopped = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        agent.velocity = Vector3.zero;
        agent.Warp(startingPoint);
        agent.isStopped = false;
        Patrol();
    }
    public void TakeDamage(){
        health--;
        if(health <= 0){
            mazeGenerator.RespawnEnemy();
            GetComponent<AudioSource>().PlayOneShot(deathSound);
            Destroy(gameObject);
        }
    }
    /**
    * Destination cell for patrolling
    
    private Vector3 startingPoint;
    private Rigidbody rb;
    public float[] rotationList = {90.0f, 180.0f, 270.0f};
    public float moveSpeed = 1.0f;

    void Start(){
        startingPoint = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void Reset(){
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = startingPoint;
    }

    /**
    * Move straight until close to wall; Rotate randomly
    
    void Update()
    {
        rb.velocity = transform.forward * moveSpeed;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.distance <= 0.5f)
            {
                float rotation = rotationList[Random.Range(0,3)];
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotation, 0);
            }
        }
        else
        {
            float rotation = rotationList[Random.Range(0,3)];
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotation, 0); // turn if face to entrence or exit
        }
    }*/
}
