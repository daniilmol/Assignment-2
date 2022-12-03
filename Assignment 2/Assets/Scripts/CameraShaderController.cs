using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderController : MonoBehaviour
{
    private FullScreenShader fullScreenShader;
    private FogScreenShader fogScreenShader;
    private FlashlightShader flashlightShader;

    // Start is called before the first frame update
    void Start()
    {
        fullScreenShader = this.GetComponent<FullScreenShader>();
        fogScreenShader = this.GetComponent<FogScreenShader>();
        flashlightShader = this .GetComponent<FlashlightShader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            fullScreenShader.enabled = !fullScreenShader.enabled;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            fogScreenShader.enabled = !fogScreenShader.enabled;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightShader.enabled = !flashlightShader.enabled;
        }
    }
}
