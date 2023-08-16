using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessingScript : MonoBehaviour
{
    [Range (0f,1f)]
    public float intensity;
    [Range(0f, 1f)]
    public float threshold;
    public float pixelizeAmount;
    public Shader grayScaleShader;
    private Material material;

    void Awake()
    {
        material = new Material(grayScaleShader);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        float thing = pixelizeAmount;
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetVector("_res", new Vector2(Screen.width, Screen.height));

        material.SetFloat("_bwBlend", intensity);
        material.SetFloat("_lumCutoff", threshold);
        material.SetFloat("_adjustAmount", thing);
        Graphics.Blit(source, destination, material);
    }
}
