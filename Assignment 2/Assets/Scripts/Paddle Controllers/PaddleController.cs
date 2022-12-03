using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PaddleController : MonoBehaviour
{
    protected InputActions inputActions;
    protected InputAction movement;
    protected InputAction movement2;
    protected Rigidbody rb;
    public int playerID;
    public int score = 0;
    public int winCondation = 5;
    public TextMeshProUGUI winMsg;
    public TextMeshProUGUI scoreBoard;
    public GameObject backButton;
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball") {
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-collision.gameObject.transform.position.x, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
        }
    }
    private void reflectBall() { 
    
    }
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputActions();
        movement = inputActions.Paddle.Movement;
        movement2 = inputActions.Paddle.Movement2;
    }

    private void OnEnable() {
        movement.Enable();
        movement2.Enable();

    }

    private void OnDisable() {
        movement.Disable();
        movement2.Disable();
    }

    public int getPlayerID() {
        return playerID;
    }

    public void incrementScore() {
        score++;
        Debug.Log("Player " + playerID + ": " + score);
    }
    public int getScore() {
        return score;
    }
    public void changeWinCondation(int n) {
        if (n >= 1) {
            winCondation = n;
        }
    }
    public void checkWin() {
        if (score >= winCondation && score >= 1) {
            Debug.Log(playerID);
            Time.timeScale = 0;
            if (playerID == 1)
            {
                winMsg.GetComponent<TextMeshProUGUI>().text = "Blue Player Wins!";
                winMsg.enabled = true;
                SceneManager.LoadScene("Maze");
                Time.timeScale = 1;
            }
            else {
                winMsg.GetComponent<TextMeshProUGUI>().text = "Red Player Wins!";
                winMsg.enabled = true;
                SceneManager.LoadScene("Maze");
                Time.timeScale = 1;
            }
            backButton.SetActive(true);
            for (int a = 0; a < backButton.transform.childCount; a++)
            {
                backButton.transform.GetChild(a).gameObject.SetActive(true);
            }
        }
    }
    public void ModifyScore(int num)
    {
        score = num;
        scoreBoard.text = getScore().ToString();
    }
}
