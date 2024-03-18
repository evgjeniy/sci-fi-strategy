using System.Collections.Generic;
using UnityEngine.UI;
using Zenject;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BuildingUICreateFactory : IFactory<IPlaceholder, BuildingType, BuildingCreateMenu>
    {
        private readonly IInstantiator _instantiator;

        public BuildingUICreateFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public BuildingCreateMenu Create(IPlaceholder parent, BuildingType type)
        {
            var createMenu = _instantiator.InstantiatePrefabResourceForComponent<BuildingCreateMenu>
            (
                Const.ResourcePath.Buildings.Prefabs.BuildingCreateMenu,
                parent.transform
            );

            foreach (var button in CreatedButtons(parent, type)) 
                createMenu.AddButton(button);
            
            return createMenu;
        }

        private IEnumerable<Button> CreatedButtons(IPlaceholder placeholder, BuildingType type)
        {
            if ((type & BuildingType.Rocket) != 0)
            {
                var turretButton = _instantiator.InstantiatePrefabResourceForComponent<Button>(BuildingType.Rocket.GetCreateButtonPath());
                turretButton.onClick.AddListener(() =>
                {
                    var rocket = _instantiator.InstantiatePrefabResourceForComponent<Building>(Const.ResourcePath.Buildings.Prefabs.Rocket, placeholder.transform);
                    placeholder.SetBuilding(rocket);
                });
                yield return turretButton;
            }

            if ((type & BuildingType.Laser) != 0)
            {
                var laserButton = _instantiator.InstantiatePrefabResourceForComponent<Button>(Const.ResourcePath.Buildings.Prefabs.Laser);
                yield return laserButton;
            }

            if ((type & BuildingType.Artillery) != 0)
            {
                var artilleryButton = _instantiator.InstantiatePrefabResourceForComponent<Button>(Const.ResourcePath.Buildings.Prefabs.Artillery);
                yield return artilleryButton;
            }

            if ((type & BuildingType.Barrack) != 0)
            {
                var barrackButton = _instantiator.InstantiatePrefabResourceForComponent<Button>(Const.ResourcePath.Buildings.Prefabs.Barrack);
                yield return barrackButton;
            }
        }
    }
}