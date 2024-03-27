using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryManagementMenu : BuildingManagementMenu
    {
        [Inject] private Artillery _artillery;

        private void OnEnable()
        {
            SubscribeBaseEvents(_artillery);

            _artillery.Data.Config.Changed += OnConfigChanged;
        }

        private void OnDisable()
        {
            UnsubscribeBaseEvents(_artillery);

            _artillery.Data.Config.Changed -= OnConfigChanged;
        }
    }
}