using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    bool f = false;
    [SerializeField] private AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            bgm.Play();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            bgm.Stop();
        }
    }
}
