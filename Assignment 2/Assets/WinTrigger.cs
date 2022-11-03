using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            canvas.GetComponent<Canvas>().enabled = true;
        }
    }

    public void RestartGame()
    {
        Debug.Log("RESTART");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
