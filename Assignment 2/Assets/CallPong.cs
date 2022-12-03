using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallPong : MonoBehaviour
{
    private GameObject player;

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider cube)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        player.transform.position -= player.transform.forward;
        SceneManager.LoadScene("PongMenu");
    }
}
