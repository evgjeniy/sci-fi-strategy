using UnityEngine;

namespace SustainTheStrain.Configs
{
    public abstract class TypedConfig<TType> : ScriptableObject
    {
        [field: SerializeField] public TType Type { get; private set; }
    }
}