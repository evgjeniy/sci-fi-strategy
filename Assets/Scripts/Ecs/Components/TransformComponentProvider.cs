using UnityEngine;
using Voody.UniLeo;

public class TransformComponentProvider : MonoProvider<TransformReference> {}

[System.Serializable]
public struct TransformReference
{
    public Transform Transform;
}