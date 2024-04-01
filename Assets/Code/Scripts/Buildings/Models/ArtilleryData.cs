using SustainTheStrain.Abilities;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class ArtilleryData
    {
        public readonly Outline Outline;
        public readonly IZoneVisualizer RadiusVisualizer;
        public readonly Observable<ArtilleryBuildingConfig> Config;
        public readonly Observable<Vector3> Orientation;
        public readonly Timer Timer;
        public readonly Area<Damageble> Area;

        public Transform ProjectileSpawnPoint { get; set; }

        public ArtilleryData(ArtilleryBuildingConfig startConfig, Outline outline, IZoneVisualizer radiusVisualizer, Vector3 startOrientation)
        {
            Outline = outline;
            RadiusVisualizer = radiusVisualizer;

            Orientation = new Observable<Vector3>(startOrientation);
            Config = new Observable<ArtilleryBuildingConfig>(startConfig);
            Timer = new Timer(startConfig.Cooldown);
            Area = new Area<Damageble>(conditions: damageable => damageable.Team != Team.Player);
        }
    }
}