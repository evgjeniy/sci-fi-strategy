using SustainTheStrain.Configs.Buildings;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class ArtilleryManagementMenu : BuildingManagementMenu<ArtilleryBuildingConfig>
    {
        [Inject] protected override IBuilding Building { get; }
    }
}