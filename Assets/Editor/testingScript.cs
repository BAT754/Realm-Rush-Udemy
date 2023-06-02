using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingScript : MonoBehaviour
{
    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        Color color = mesh.material.color;

        color = new Color(color.r, color.g, color.b, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

