namespace SustainTheStrain._Contracts.BuildingSystem
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
        public static string GetPrefabPath(this BuildingType type) => type switch
        {
            BuildingType.Rocket => Const.ResourcePath.Buildings.Prefabs.Rocket,
            BuildingType.Laser => Const.ResourcePath.Buildings.Prefabs.Laser,
            BuildingType.Artillery => Const.ResourcePath.Buildings.Prefabs.Artillery,
            BuildingType.Barrack => Const.ResourcePath.Buildings.Prefabs.Barrack,
            _ => throw new System.ArgumentOutOfRangeException(nameof(type), type, $"Can't get create button path by building type: {type}")
        };
    }
}