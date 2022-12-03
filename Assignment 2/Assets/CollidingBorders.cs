using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingBorders : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(collision.gameObject.GetComponent<Rigidbody>().velocity.x , -collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
        }
    }
}
