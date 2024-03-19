using UnityEngine;
using UnityEngine.UI;

namespace SustainTheStrain._Contracts.BuildingSystem
{
    public class BuildingCreateMenu : MonoBehaviour
    {
        [field: SerializeField] public Button CreateRocket { get; private set; }
        [field: SerializeField] public Button CreateLaser { get; private set; }
        [field: SerializeField] public Button CreateArtillery { get; private set; }
        [field: SerializeField] public Button CreateBarrack { get; private set; }
    }
}