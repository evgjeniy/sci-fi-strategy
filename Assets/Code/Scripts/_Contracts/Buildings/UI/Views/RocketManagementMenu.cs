using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketManagementMenu : BuildingManagementMenu
    {
        [Inject] private Rocket _rocket;

        private void OnEnable()
        {
            SubscribeBaseEvents(_rocket);

            _rocket.Data.Config.Changed += OnConfigChanged;
        }

        private void OnDisable()
        {
            _rocket.Data.Config.Changed -= OnConfigChanged;

            UnsubscribeBaseEvents(_rocket);
        }
    }
}