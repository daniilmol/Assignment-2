using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallPong : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            col.transform.position -= col.transform.forward;
            SceneManager.LoadScene("PongMenu");
        }
    }
}
