using SustainTheStrain.Configs;
using SustainTheStrain.Configs.Buildings;
using UnityEngine;
using UnityEngine.Extensions;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BuildingFactory : IBuildingFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IConfigProviderService _configProviderService;

        public BuildingFactory(IInstantiator instantiator, IConfigProviderService configProviderService)
        {
            _instantiator = instantiator;
            _configProviderService = configProviderService;
        }

        public BuildingSelectorMenu CreateSelector(IPlaceholder placeholder)
        {
            return _instantiator.InstantiatePrefabResourceForComponent<BuildingSelectorMenu>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.BuildingSelectorMenu,
                extraArgs: new[] { placeholder }
            );
        }
        
        public Rocket CreateRocket(IPlaceholder placeholder)
        {
            var startConfig = _configProviderService.GetBuildingConfig<RocketBuildingConfig>();
            var startOrientation = placeholder.Road.Project(placeholder.transform.position);
            
            var observableConfig = new Observable<RocketBuildingConfig>(startConfig);
            var observableOrientation = new Observable<Vector3>(startOrientation);
            var observableSelection = new Observable<SelectionType>();
            
            var rocket = _instantiator.InstantiatePrefabResourceForComponent<Rocket>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Rocket,
                parentTransform: placeholder.transform,
                extraArgs: new object[] { observableConfig, observableOrientation, observableSelection }
            );
            
            BuildingRotator graphics = null;
            observableConfig.Changed += config =>
            {
                graphics.IfNotNull(x => x.DestroyObject());
                graphics = _instantiator.InstantiatePrefabResourceForComponent<BuildingRotator>
                (
                    resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/GFX/Rocket [Level {config.Level}]",
                    parentTransform: rocket.transform,
                    extraArgs: new object[] { observableOrientation }
                );
                rocket.SpawnPointProvider = graphics;
            };

            var rocketOutline = _instantiator.InstantiateComponent<BuildingOutline>
            (
                gameObject: rocket.gameObject,
                extraArgs: new object[] { observableSelection }
            );

            var rocketRadiusVisualizer = _instantiator.InstantiatePrefabResourceForComponent<RocketRadiusVisualizer>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.RocketRadiusVisualizer,
                parentTransform: rocket.transform,
                extraArgs: new object[] { observableConfig, observableSelection, observableOrientation  }
            );

            var rocketManagementMenu = _instantiator.InstantiatePrefabResourceForComponent<RocketManagementMenu>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.RocketManagementMenu,
                parentTransform: rocket.transform,
                extraArgs: new object[] { rocket, observableConfig, observableSelection }
            ).With(x => x.transform.LookAtCamera(rocket.transform));
            
            return rocket;
        }

        public Laser CreateLaser(IPlaceholder placeholder)
        {
            var startConfig = _configProviderService.GetBuildingConfig<LaserBuildingConfig>();
            var startOrientation = placeholder.Road.Project(placeholder.transform.position);
            
            var observableConfig = new Observable<LaserBuildingConfig>(startConfig);
            var observableOrientation = new Observable<Vector3>(startOrientation);
            var observableSelection = new Observable<SelectionType>();
            
            var laser = _instantiator.InstantiatePrefabResourceForComponent<Laser>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Laser,
                parentTransform: placeholder.transform,
                extraArgs: new object[] { observableConfig, observableOrientation, observableSelection }
            );

            BuildingRotator graphics = null;
            observableConfig.Changed += config =>
            {
                graphics.IfNotNull(x => x.DestroyObject());
                graphics = _instantiator.InstantiatePrefabResourceForComponent<BuildingRotator>
                (
                    resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/GFX/Laser [Level {config.Level}]",
                    parentTransform: laser.transform,
                    extraArgs: new object[] { observableOrientation }
                );
                laser.SpawnPointProvider = graphics;
            };

            var outline = _instantiator.InstantiateComponent<BuildingOutline>
            (
                gameObject: laser.gameObject,
                extraArgs: new object[] { observableSelection }
            );

            var radiusVisualizer = _instantiator.InstantiatePrefabResourceForComponent<LaserRadiusVisualizer>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.LaserRadiusVisualizer,
                parentTransform: laser.transform,
                extraArgs: new object[] { observableConfig, observableSelection  }
            );

            var managementMenu = _instantiator.InstantiatePrefabResourceForComponent<LaserManagementMenu>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.LaserManagementMenu,
                parentTransform: laser.transform,
                extraArgs: new object[] { laser, observableConfig, observableSelection }
            ).With(x => x.transform.LookAtCamera(from: laser.transform));
            
            return laser;
        }

        public Artillery CreateArtillery(IPlaceholder placeholder)
        {
            var startConfig = _configProviderService.GetBuildingConfig<ArtilleryBuildingConfig>();
            var startOrientation = placeholder.Road.Project(placeholder.transform.position);
            
            var observableConfig = new Observable<ArtilleryBuildingConfig>(startConfig);
            var observableOrientation = new Observable<Vector3>(startOrientation);
            var observableSelection = new Observable<SelectionType>();
            
            var artillery = _instantiator.InstantiatePrefabResourceForComponent<Artillery>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Artillery,
                parentTransform: placeholder.transform,
                extraArgs: new object[] { observableConfig, observableOrientation, observableSelection }
            );

            BuildingRotator graphics = null;
            observableConfig.Changed += config =>
            {
                graphics.IfNotNull(x => x.DestroyObject());
                graphics = _instantiator.InstantiatePrefabResourceForComponent<BuildingRotator>
                (
                    resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/GFX/Artillery [Level {config.Level}]",
                    parentTransform: artillery.transform,
                    extraArgs: new object[] { observableOrientation }
                );
                artillery.SpawnPointProvider = graphics;
            };

            var outline = _instantiator.InstantiateComponent<BuildingOutline>
            (
                gameObject: artillery.gameObject,
                extraArgs: new object[] { observableSelection }
            );

            var radiusVisualizer = _instantiator.InstantiatePrefabResourceForComponent<ArtilleryRadiusVisualizer>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.ArtilleryRadiusVisualizer,
                parentTransform: artillery.transform,
                extraArgs: new object[] { observableConfig, observableSelection  }
            );

            var managementMenu = _instantiator.InstantiatePrefabResourceForComponent<ArtilleryManagementMenu>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.ArtilleryManagementMenu,
                parentTransform: artillery.transform,
                extraArgs: new object[] { artillery, observableConfig, observableSelection }
            ).With(x => x.transform.LookAtCamera(from: artillery.transform));
            
            return artillery;
        }

        public Barrack CreateBarrack(IPlaceholder placeholder)
        {
            var startConfig = _configProviderService.GetBuildingConfig<BarrackBuildingConfig>();
            var startOrientation = placeholder.Road.Project(placeholder.transform.position);
            
            var observableConfig = new Observable<BarrackBuildingConfig>(startConfig);
            var observableSpawnPoint = new Observable<Vector3>(startOrientation);
            var observableSelection = new Observable<SelectionType>();
            
            var barrack = _instantiator.InstantiatePrefabResourceForComponent<Barrack>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.Barrack,
                parentTransform: placeholder.transform,
                extraArgs: new object[] { observableConfig, observableSpawnPoint, observableSelection }
            );

            GameObject graphics = null;
            observableConfig.Changed += config =>
            {
                graphics.IfNotNull(x => x.DestroyObject());
                graphics = _instantiator.InstantiatePrefabResource
                (
                    resourcePath: Const.ResourcePath.Buildings.Prefabs.Root + $"/GFX/Barrack [Level {config.Level}]",
                    parentTransform: barrack.transform
                );
            };

            var outline = _instantiator.InstantiateComponent<BuildingOutline>
            (
                gameObject: barrack.gameObject,
                extraArgs: new object[] { observableSelection }
            );

            var radiusVisualizer = _instantiator.InstantiatePrefabResourceForComponent<BarrackRadiusVisualizer>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.BarrackRadiusVisualizer,
                parentTransform: barrack.transform,
                extraArgs: new object[] { observableConfig, observableSelection  }
            );

            var managementMenu = _instantiator.InstantiatePrefabResourceForComponent<BarrackManagementMenu>
            (
                resourcePath: Const.ResourcePath.Buildings.Prefabs.BarrackManagementMenu,
                parentTransform: barrack.transform,
                extraArgs: new object[] { barrack, observableConfig, observableSelection }
            ).With(x => x.transform.LookAtCamera(from: barrack.transform));
            
            return barrack;
        }
    }
}