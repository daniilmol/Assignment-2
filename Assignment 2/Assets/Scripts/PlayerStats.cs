using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    private float score;
    private float x;
    private float y;

    public float GetScore(){
        return score;
    }

    public void IncrementScore(){
        score++;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy"){
            SceneManager.LoadScene("maze");
        }
    }
}
