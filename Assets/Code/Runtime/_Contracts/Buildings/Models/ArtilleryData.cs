using System;
using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain.Abilities;
using UnityEngine;

namespace SustainTheStrain._Contracts.Buildings
{
    public class ArtilleryData
    {
        public readonly Outline Outline;
        public readonly Observable<ArtilleryBuildingConfig> Config;
        public readonly Observable<Vector3> Orientation;

        public ArtilleryData(ArtilleryBuildingConfig startConfig, Outline outline)
        {
            Outline = outline;
            Config = new Observable<ArtilleryBuildingConfig>(startConfig);
            Orientation = new Observable<Vector3>();
        }
    }
}