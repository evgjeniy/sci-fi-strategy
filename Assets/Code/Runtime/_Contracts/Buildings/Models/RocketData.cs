using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain.Abilities;
using UnityEngine;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketData
    {
        public readonly Outline Outline;
        public readonly Observable<RocketBuildingConfig> Config;
        public readonly Observable<Vector3> Orientation;

        public RocketData(RocketBuildingConfig startConfig, Outline outline)
        {
            Outline = outline;
            Config = new Observable<RocketBuildingConfig>(startConfig);
            Orientation = new Observable<Vector3>();
        }
    }
}