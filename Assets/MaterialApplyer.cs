using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialApplyer : MonoBehaviour
{
    [SerializeField] private Material _material;

    [Button("Apply")]
    private void ApplyMaterial()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.material = _material;
        }
    }
}
