using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start() {
        DontDestroyOnLoadManager.DestroyAll();
    }

    public void startGame(string sceneName) {
        SceneManager.LoadScene("maze");
    }

    public void exitGame() {
        Application.Quit();
    }
}
