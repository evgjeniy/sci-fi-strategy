using SustainTheStrain.Configs.Buildings;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class LaserManagementMenu : BuildingManagementMenu<LaserBuildingConfig>
    {
        [Inject] protected override IBuilding Building { get; }
    }
}