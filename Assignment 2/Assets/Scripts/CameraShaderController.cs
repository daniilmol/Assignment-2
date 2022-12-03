using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderController : MonoBehaviour
{
    private FullScreenShader fullScreenShader;
    private FogScreenShader fogScreenShader;
    private FlashlightShader flashlightShader;
    private BGMController bgmController;

    // Start is called before the first frame update
    void Start()
    {
        fullScreenShader = this.GetComponent<FullScreenShader>();
        fogScreenShader = this.GetComponent<FogScreenShader>();
        flashlightShader = this .GetComponent<FlashlightShader>();
        bgmController = GameObject.Find("BGMController").GetComponent<BGMController>();
        bgmController.PlayDayDgm();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            fullScreenShader.enabled = !fullScreenShader.enabled;

            if (fullScreenShader.enabled)
            {
                bgmController.StopDayDgm();
                bgmController.PlayNightBgm();
            }
            else
            {
                bgmController.StopNightBgm();
                bgmController.PlayDayDgm();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            fogScreenShader.enabled = !fogScreenShader.enabled;

            if (fogScreenShader.enabled)
            {
                bgmController.SetDayBgmVolume(0.25f);
                bgmController.SetNightVolume(0.25f);
                bgmController.SetBgmVolume(0.25f);
            }
            else
            {
                bgmController.SetDayBgmVolume(0.5f);
                bgmController.SetNightVolume(0.5f);
                bgmController.SetBgmVolume(0.5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightShader.enabled = !flashlightShader.enabled;
        }
    }
}
