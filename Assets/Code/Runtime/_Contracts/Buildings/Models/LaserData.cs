using System;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain.Abilities;
using UnityEngine;

namespace SustainTheStrain._Contracts.Buildings
{
    public class LaserData
    {
        public readonly Outline Outline;
        public readonly Observable<LaserBuildingConfig> Config;
        public readonly Observable<Vector3> Orientation;

        public LaserData(LaserBuildingConfig startConfig, Outline outline)
        {
            Outline = outline;
            Config = new Observable<LaserBuildingConfig>(startConfig);
            Orientation = new Observable<Vector3>();
        }
    }
}