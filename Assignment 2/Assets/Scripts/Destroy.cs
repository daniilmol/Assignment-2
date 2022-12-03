using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    public GameObject ReplayButton;

    void Awake()
    {
        ReplayButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //   // Application.LoadLevel(0);
        //    SceneManager.LoadScene(0);
        //}
    }
}
