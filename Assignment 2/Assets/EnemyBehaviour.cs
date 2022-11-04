using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    /**
    * Destination cell for patrolling
    */
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
    */
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
    }
}
