﻿using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain.Abilities;
using SustainTheStrain.Units;
using UnityEngine;
using Timer = SustainTheStrain.Buildings.FSM.Timer;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryData
    {
        public readonly Outline Outline;
        public readonly Observable<ArtilleryBuildingConfig> Config;
        public readonly Observable<Vector3> Orientation;
        public readonly Timer Timer;
        public readonly Area<Damageble> Area;

        public ArtilleryData(ArtilleryBuildingConfig startConfig, Outline outline)
        {
            Outline = outline;
            Orientation = new Observable<Vector3>();
            Config = new Observable<ArtilleryBuildingConfig>(startConfig);
            Timer = new Timer(startConfig.Cooldown);
            Area = new Area<Damageble>(conditions: damageable => damageable.Team != 1);
        }
    }
}