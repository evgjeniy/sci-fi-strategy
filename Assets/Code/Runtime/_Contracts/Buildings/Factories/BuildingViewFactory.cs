using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IBuildingViewFactory
    {
        public TView Create<TView, TModel>(TModel model, string configName = null)
            where TView : BuildingMenuView where TModel : class;
    }

    public class BuildingViewFactory : IBuildingViewFactory
    {
        private readonly IInstantiator _instantiator;

        public BuildingViewFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public TView Create<TView, TModel>(TModel model, string configName = null)
            where TView : BuildingMenuView where TModel : class
        {
            configName ??= typeof(TView).Name;
            var path = Const.ResourcePath.Buildings.Prefabs.Root + "/UI/" + configName;
            return _instantiator.InstantiatePrefabResourceForComponent<TView>(path, new []{ model });
        }
    }
}