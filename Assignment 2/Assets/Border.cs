using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] GameObject oppositePlayer;
    [SerializeField] GameObject ball;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball") {
            oppositePlayer.GetComponent<PaddleController>().incrementScore();
            Destroy(other.gameObject);
            Instantiate(ball, new Vector3(0, 0, 3.9f), Quaternion.identity);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
