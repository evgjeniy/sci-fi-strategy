using SustainTheStrain.Buildings.UI.Menus;
using UnityEngine.Events;

namespace SustainTheStrain.Buildings.UI.Buttons.Management
{
    public class DestroyBuildingButton : BaseBuildingButton<IBuildingManagementMenu>
    {
        protected override UnityAction ButtonAction => Menu.DestroyRequest;
    }
}