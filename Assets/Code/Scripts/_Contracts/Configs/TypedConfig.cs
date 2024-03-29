using UnityEngine;

namespace SustainTheStrain._Contracts.Configs
{
    public abstract class TypedConfig<TType> : ScriptableObject
    {
        [field: SerializeField] public TType Type { get; private set; }
    }
}