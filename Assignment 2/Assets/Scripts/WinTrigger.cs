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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackMainManu() {
        SceneManager.LoadScene("Main");
    }
}
