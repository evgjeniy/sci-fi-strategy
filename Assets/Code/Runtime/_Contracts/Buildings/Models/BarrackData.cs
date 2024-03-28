﻿using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain.Abilities;
using SustainTheStrain.Units;
using SustainTheStrain.Units.Spawners;
using UnityEngine;
using Timer = SustainTheStrain.Buildings.FSM.Timer;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BarrackData
    {
        public readonly Observable<BarrackBuildingConfig> Config;
        public readonly Outline Outline;
        public readonly RecruitGroup RecruitGroup;
        public readonly RecruitSpawner RecruitSpawner;
        public readonly Timer Timer;

        public GameObject RecruitsPointer { get; set; }

        public BarrackData(BarrackBuildingConfig startConfig, Outline outline, RecruitGroup recruitGroup, RecruitSpawner recruitSpawner)
        {
            Outline = outline;
            RecruitGroup = recruitGroup;
            RecruitSpawner = recruitSpawner;
            Config = new Observable<BarrackBuildingConfig>(startConfig);
            Timer = new Timer(startConfig.RespawnCooldown);
        }
    }
}