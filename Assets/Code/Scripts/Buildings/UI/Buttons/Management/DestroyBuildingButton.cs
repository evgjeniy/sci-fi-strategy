using UnityEngine.Events;

namespace SustainTheStrain.Buildings.UI
{
    public class DestroyBuildingButton : BaseBuildingButton<IBuildingManagementMenu>
    {
        protected override UnityAction ButtonAction => Menu.DestroyRequest;
    }
}