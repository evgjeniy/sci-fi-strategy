using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IBuildingViewFactory
    {
        public TView Create<TView, TBuilding>(TBuilding building, string configName = null)
            where TView : BuildingMenuView where TBuilding : class, IBuilding;
    }

    public class BuildingViewFactory : IBuildingViewFactory
    {
        private readonly IInstantiator _instantiator;

        public BuildingViewFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public TView Create<TView, TBuilding>(TBuilding building, string configName = null)
            where TView : BuildingMenuView where TBuilding : class, IBuilding
        {
            var path = Const.ResourcePath.Buildings.Prefabs.Root + $"/UI/{configName ?? typeof(TView).Name}";
            return _instantiator.InstantiatePrefabResourceForComponent<TView>(path, new []{ building })
                .With(x => x.SetParent(building.transform))
                .With(x => x.transform.LookAtCamera(building.transform));;
        }
    }
}