using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BuildingUICreateFactory : IFactory<IPlaceholder, BuildingCreateMenu>
    {
        private readonly IInstantiator _instantiator;

        public BuildingUICreateFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public BuildingCreateMenu Create(IPlaceholder placeholder)
        {
            var createMenu = _instantiator.InstantiatePrefabResourceForComponent<BuildingCreateMenu>(
                Const.ResourcePath.Buildings.Prefabs.BuildingCreateMenu, placeholder.transform);

            createMenu.Placeholder = placeholder;
            return createMenu;
        }
    }
}