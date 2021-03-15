using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
    public Texture[] characterRotationTextures;
    public float[] bucketStartAngles;
    
    private Quaternion rotation;
    private Material material;

    void Awake()
    {
        rotation = transform.rotation;
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotation;
        for (int i = 0; i < characterRotationTextures.Length; i++)
        {
            if (bucketStartAngles[i] > transform.parent.eulerAngles.y)
            {
                material.SetTexture("_BaseMap", characterRotationTextures[i]);
                return;
            }
        }
        material.SetTexture("_BaseMap", characterRotationTextures[0]);
    }
}
