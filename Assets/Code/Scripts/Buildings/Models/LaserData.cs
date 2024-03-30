using SustainTheStrain.Abilities;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class LaserData
    {
        public readonly Outline Outline;
        public readonly IZoneVisualizer RadiusVisualizer;
        public readonly Observable<LaserBuildingConfig> Config;
        public readonly Observable<Vector3> Orientation;
        public readonly Timer Timer;
        public readonly Area<Damageble> Area;

        public LaserData(LaserBuildingConfig startConfig, Outline outline, IZoneVisualizer radiusVisualizer)
        {
            Outline = outline;
            RadiusVisualizer = radiusVisualizer;
            
            Orientation = new Observable<Vector3>();
            Config = new Observable<LaserBuildingConfig>(startConfig);
            Timer = new Timer(startConfig.Cooldown);
            Area = new Area<Damageble>(conditions: damageable => damageable.Team != Team.Player);
        }
    }
}