using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain.Abilities;
using SustainTheStrain.Units;
using UnityEngine;
using Timer = SustainTheStrain.Buildings.FSM.Timer;

namespace SustainTheStrain._Contracts.Buildings
{
    public class RocketData
    {
        public readonly Outline Outline;
        public readonly Observable<RocketBuildingConfig> Config;
        public readonly Observable<Vector3> Orientation;
        public readonly Timer Timer;
        public readonly Area<Damageble> Area;

        public Transform ProjectileSpawnPoint { get; set; }

        public RocketData(RocketBuildingConfig startConfig, Outline outline)
        {
            Outline = outline;
            Orientation = new Observable<Vector3>();
            Config = new Observable<RocketBuildingConfig>(startConfig);
            Timer = new Timer(startConfig.Cooldown);
            Area = new Area<Damageble>(conditions: damageable => damageable.Team != 1);
        }
    }
}