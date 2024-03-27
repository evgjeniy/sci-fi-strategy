using System;
using SustainTheStrain._Contracts.Configs.Buildings;

namespace SustainTheStrain._Contracts.Buildings
{
    public interface IBuildingData
    {
        public BuildingConfig Config { get; }
        public event Action<BuildingConfig> ConfigChanged;
    }
}