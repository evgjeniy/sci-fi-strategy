namespace SustainTheStrain._Contracts.Buildings
{
    [System.Flags]
    public enum BuildingType : byte
    {
        All = byte.MaxValue,
        None = 0,
        
        Rocket = 1,
        Laser = 2,
        Artillery = 4,
        Barrack = 8
    }

    public static class BuildingTypeExtensions
    {
        public static string GetCreateButtonPath(this BuildingType type) => type switch
        {
            BuildingType.Rocket => Const.ResourcePath.Buildings.Prefabs.RocketCreateButton,
            BuildingType.Laser => Const.ResourcePath.Buildings.Prefabs.LaserCreateButton,
            BuildingType.Artillery => Const.ResourcePath.Buildings.Prefabs.ArtilleryCreateButton,
            BuildingType.Barrack => Const.ResourcePath.Buildings.Prefabs.BarrackCreateButton,
            _ => throw new System.ArgumentOutOfRangeException(nameof(type), type, $"Can't get create button path by building type: {type}")
        };
    }
}