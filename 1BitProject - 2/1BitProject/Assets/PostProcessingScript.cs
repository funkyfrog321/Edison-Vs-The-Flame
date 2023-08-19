using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessingScript : MonoBehaviour
{
    public Color color1 = Color.white;
    public Color color2 = Color.black;

    [Range (0f,1f)]
    public float intensity;
    [Range(0f, 1f)]
    public float threshold;
    public float pixelizeAmount;
    public Shader shader;
    private Material material;

    Matrix4x4 dither = new Matrix4x4(new Vector4(0, 8, 2, 10) * 0.0625f,
                                new Vector4(12, 4, 14, 6) * 0.0625f,
                                new Vector4(3, 11, 1, 9) * 0.0625f,
                                new Vector4(15, 7, 13, 5) * 0.0625f);

    void Awake()
    {
        material = new Material(shader);

        material.SetMatrix("_bayer", dither);
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
        material.SetColor("_color1", color1);
        material.SetColor("_color2", color2);
        Graphics.Blit(source, destination, material);
    }
}
