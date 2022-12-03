using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CommandPrompt : MonoBehaviour
{
    public void OpenConsole()
    {
        Time.timeScale = 0;
        this.GetComponent<Renderer>().enabled = true;
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(true);
        }
    }

    public void ExitConsole()
    {
        this.GetComponent<Renderer>().enabled = false;
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }
}
