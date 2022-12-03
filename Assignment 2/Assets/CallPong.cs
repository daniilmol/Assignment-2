using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallPong : MonoBehaviour
{
    void OnTriggerEnter(Collider cube)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("PongMenu");
    }
}
