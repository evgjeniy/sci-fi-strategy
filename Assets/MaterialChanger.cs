using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private Material _material;
    
    [Button("Apply")]
    public void ApplyMaterial()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = _material;
        }
    }
}
