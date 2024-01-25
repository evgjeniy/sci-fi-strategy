using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public interface IBuildingSelector {}

    public class BuildingSelector : MonoBehaviour, IBuildingSelector
    {
        [Zenject.Inject]
        private void Construct(IBuildingSystem buildingSystem/*, IBuildingInputService inputService*/ )
        {
            // send Create/Upgrade messages to BuildingSystem
        }
    }
}