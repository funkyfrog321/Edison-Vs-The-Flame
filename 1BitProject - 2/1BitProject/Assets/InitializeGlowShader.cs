using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InitializeGlowShader : MonoBehaviour
{
    private Material m_material;
    Camera camera;
    Vector3 lastPosition;
    Matrix4x4 dither;
    //public static List<Transform> lights = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        m_material = gameObject.GetComponent<Renderer>().material;

        m_material.SetVector("_res", new Vector2(Screen.width, Screen.height));

        lastPosition = transform.position;

        dither = new Matrix4x4(new Vector4(0, 8, 2, 10) * 0.0625f,
                                new Vector4(12, 4, 14, 6) * 0.0625f,
                                new Vector4(3, 11, 1, 9) * 0.0625f,
                                new Vector4(15, 7, 13, 5) * 0.0625f);

        //lights.Add(transform);
    }

    //void PrintAllLightTransforms()
    //{
    //    Debug.Log("All of the lights");
    //    foreach (Transform t in lights)
    //    {
    //        Debug.Log(t.position);
    //    }
    //}

    //public void RemoveLight()
    //{
    //    lights.Remove(transform);
    //}

    // Update is called once per frame
    void Update()
    {
        Vector3 precenter = camera.WorldToScreenPoint(transform.position);
        if (precenter != lastPosition)
        {
            Vector4 center = new Vector4(precenter.x, precenter.y, precenter.z, 0.0f);
            m_material.SetVector("_center", center);
            m_material.SetMatrix("_ordered_bayer", dither);
            lastPosition = precenter;
        }
    }
}
