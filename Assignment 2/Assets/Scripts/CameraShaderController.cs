using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderController : MonoBehaviour
{
    private FullScreenShader fullScreenShader;
    private FogScreenShader fogScreenShader;
    [SerializeField] private AudioSource dayBgm;
    [SerializeField] private AudioSource nighBgm;

    // Start is called before the first frame update
    void Start()
    {
        fullScreenShader = this.GetComponent<FullScreenShader>();
        fogScreenShader = this.GetComponent<FogScreenShader>();
        dayBgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            fullScreenShader.enabled = !fullScreenShader.enabled;
            
            if(fullScreenShader.enabled)
            {
                dayBgm.Stop();
                nighBgm.Play();
            }
            else
            {
                nighBgm.Stop();
                dayBgm.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            fogScreenShader.enabled = !fogScreenShader.enabled;

            if(fogScreenShader.enabled)
            {
                dayBgm.volume = 0.5f;
                nighBgm.volume = 0.5f;
            }
            else
            {
                dayBgm.volume = 1f;
                nighBgm.volume = 1f;
            }
        }
    }
}
