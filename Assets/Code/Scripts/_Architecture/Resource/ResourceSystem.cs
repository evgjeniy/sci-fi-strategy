using UnityEngine;

namespace SustainTheStrain._Architecture.Resource
{
    public class ResourceSystem : MonoBehaviour, IController<ResourceData, ResourceView>
    {
        [field: SerializeField] public ResourceData Model { get; private set; }
        [field: SerializeField] public ResourceView View { get; private set; }
    }
}