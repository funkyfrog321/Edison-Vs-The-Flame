using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InitializeGlowShader : MonoBehaviour
{
    private Material m_material;
    Camera camera;
    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        m_material = gameObject.GetComponent<Renderer>().material;

        m_material.SetVector("_res", new Vector2(Screen.width, Screen.height));

        lastPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 precenter = camera.WorldToScreenPoint(transform.position);
        if (precenter != lastPosition)
        {
            Vector4 center = new Vector4(precenter.x, precenter.y, precenter.z, 0.0f);
            m_material.SetVector("_center", center);
            lastPosition = precenter;
        }
    }
}
