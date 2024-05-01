using SustainTheStrain.Configs.Buildings;
using SustainTheStrain.Input;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public interface IBuilding : ITransformable, IInputSelectable, IInputPointerable
    {
        public BuildingConfig Config { get; }

        public void Upgrade();
        public void Destroy();
    }

    public interface ITurret : IBuilding
    {
        public Area<Damageble> Area { get; }
        public Vector3 Orientation { get; set; }
    }
}