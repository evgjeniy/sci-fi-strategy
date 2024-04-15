using SustainTheStrain.Configs.Buildings;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class RocketManagementMenu : BuildingManagementMenu<RocketBuildingConfig>
    {
        [Inject] protected override IBuilding Building { get; }
    }
}