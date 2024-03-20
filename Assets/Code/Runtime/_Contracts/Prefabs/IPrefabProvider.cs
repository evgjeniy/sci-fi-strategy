namespace SustainTheStrain._Contracts.Prefabs
{
    public interface IPrefabProvider
    {
        public TPrefab GetPrefab<TPrefab>();
    }
    
    public class PrefabProvider : IPrefabProvider
    {
        public TPrefab GetPrefab<TPrefab>()
        {
            return default;
        }
    }
}