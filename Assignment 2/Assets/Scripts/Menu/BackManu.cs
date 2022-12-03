using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackManu : MonoBehaviour
{
    public GameObject btn;
    private void Awake()
    {
        btn.SetActive(false);
        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }
    }

    public void BackMainManu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
