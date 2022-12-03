using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float colorSpeed = 3f;
    public float ballYLocation = 0;

    void Start()
    {
        float yDir = Random.Range(0, 2);
        if (yDir == 0)
        {
            yDir = Random.Range(1, 3);
        }
        else if(yDir == 1){
            yDir = Random.Range(-3, -1);
        }
        float xDir = Random.Range(0, 2);
        if (xDir == 0)
        {
            xDir = Random.Range(1, 3);
        }
        else if (xDir == 1)
        {
            xDir = Random.Range(-3, -1);
        }
        GetComponent<Rigidbody>().velocity = new Vector3(xDir, yDir);
    }


    float timeLeft;
    Color targetColor;

    void FixedUpdate()
    {
        if (timeLeft <= Time.deltaTime)
        {
            GetComponent<Light>().color = targetColor;

            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = colorSpeed;
        }
        else
        {
            GetComponent<Light>().color = Color.Lerp(GetComponent<Light>().color, targetColor, Time.deltaTime / timeLeft);

            timeLeft -= Time.deltaTime;
        }

        ballYLocation = transform.position.y;
    }
}
