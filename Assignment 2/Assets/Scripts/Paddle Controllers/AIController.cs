using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIController : PaddleController
{
    private float position = 0;
    public TextMeshProUGUI redScore;

    private void Start()
    {
        if (StartGame.sg.mutilFlag)
        {
            disableAiMode();
            return;
        }
        playerID = 2;

        score = 0;
        redScore.text = 0.ToString();
    }
    private void FixedUpdate()
    {
        position = FindObjectOfType<Ball>().ballYLocation;
        Vector3 v3 = new Vector3(0, position, 0);
        transform.Translate(v3 * 0.05f);
        redScore.text = getScore().ToString();
        checkWin();
    }

    private void disableAiMode()
    {
        this.enabled = false;
    }
}
