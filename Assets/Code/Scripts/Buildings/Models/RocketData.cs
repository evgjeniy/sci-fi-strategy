﻿using SustainTheStrain.Abilities;
using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Components;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class RocketData
    {
        public readonly Outline Outline;
        public readonly IZoneVisualizer RadiusVisualizer;
        public readonly IZoneVisualizer SectorVisualizer;
        public readonly Observable<RocketBuildingConfig> Config;
        public readonly Observable<Vector3> Orientation;
        public readonly Timer Timer;
        public readonly Area<Damageble> Area;

        public Transform ProjectileSpawnPoint { get; set; }

        public RocketData(RocketBuildingConfig startConfig, Outline outline, IZoneVisualizer radiusVisualizer,
            IZoneVisualizer sectorVisualizer, Vector3 startOrientation)
        {
            Outline = outline;
            RadiusVisualizer = radiusVisualizer;
            SectorVisualizer = sectorVisualizer;

            Orientation = new Observable<Vector3>(startOrientation);
            Config = new Observable<RocketBuildingConfig>(startConfig);
            Timer = new Timer(startConfig.Cooldown);
            Area = new Area<Damageble>(conditions: damageable => damageable.Team != Team.Player);
        }
    }
}