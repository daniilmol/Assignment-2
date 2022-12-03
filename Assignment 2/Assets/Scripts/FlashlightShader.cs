using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightShader : MonoBehaviour
{
    public Shader awesomeShader = null;
    public Texture texture;
    private Material m_renderMaterial;
    
    void Start()
    {
        if (awesomeShader == null)
        {
            Debug.LogError("no awesome shader.");
            m_renderMaterial = null;
            return;
        }

        m_renderMaterial = new Material(awesomeShader);
        m_renderMaterial.SetTexture("_MainTex", texture);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_renderMaterial);
    }
}
