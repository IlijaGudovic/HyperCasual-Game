using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture : MonoBehaviour
{
    [SerializeField]
    public UnityEngine.Texture texture;

    private void Awake()
    {
        GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
    }

}
