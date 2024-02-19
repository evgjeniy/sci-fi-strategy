using UnityEngine.Events;

namespace SustainTheStrain.Buildings.UI
{
    public class UpgradeBuildingButton : BaseBuildingButton<IBuildingManagementMenu>
    {
        protected override UnityAction ButtonAction => Menu.UpgradeRequest;
    }
}