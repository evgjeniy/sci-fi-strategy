using SustainTheStrain._Contracts;
using UnityEngine;

namespace SustainTheStrain.Configs
{
    public enum UnitType { Unit, ShieldUnit, RangeUnit, Tank, ShieldTank, Kamikaze, Boss }

    [CreateAssetMenu(fileName = nameof(UnitConfig), menuName = "Configs/" + nameof(UnitConfig), order = Const.Order.UnitConfigs)]
    public class UnitConfig : TypedConfig<UnitType>
    {
        [field: SerializeField, Min(0.0f)] public float Health { get; private set; }
        [field: SerializeField, Min(0.0f)] public float Damage { get; private set; }
        [field: SerializeField, Min(0.0f)] public float Speed { get; private set; }
        [field: SerializeField, Min(0.0f)] public float Cooldown { get; private set; }
        [field: SerializeField, Min(0.0f)] public float Shield { get; private set; }
    }
}