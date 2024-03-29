using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class LaserManagementMenu : BuildingManagementMenu
    {
        [Inject] private Laser _laser;

        private void OnEnable()
        {
            SubscribeBaseEvents(_laser);
            
            _laser.Data.Config.Changed += OnConfigChanged;
        }

        private void OnDisable()
        {
            UnsubscribeBaseEvents(_laser);
            
            _laser.Data.Config.Changed -= OnConfigChanged;
        }
    }
}