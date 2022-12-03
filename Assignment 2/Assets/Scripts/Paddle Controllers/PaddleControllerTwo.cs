using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PaddleControllerTwo : PaddleController
{
    private float position;
    private void Start()
    {
        playerID = 2;
        score = 0;
        scoreBoard.text = 0.ToString();
    }
    private void FixedUpdate()
    {
        AIControl();
        PlayerControl();
        scoreBoard.text = getScore().ToString();
        checkWin();
    }

    private void AIControl() 
    {
        if (StartGame.sg.singleFlag)
        {
            movement2.Disable();
            position = FindObjectOfType<Ball>().ballYLocation;
            Vector3 v3 = new Vector3(0, position, 0);
            transform.Translate(v3 * 0.05f);
        }
    }

    private void PlayerControl()
    {
        if (StartGame.sg.mutilFlag)
        {
            movement2.Enable();
            Vector2 v2 = movement2.ReadValue<Vector2>();
            Vector3 v3 = new Vector3(0, v2.y, 0);
            transform.Translate(v3 * 0.05f);
        }
    }
}
