namespace SustainTheStrain.Buildings
{
    public interface IBuildingFactory
    {
        BuildingSelectorMenu CreateSelector(IPlaceholder placeholder);
        Rocket CreateRocket(IPlaceholder placeholder);
        Laser CreateLaser(IPlaceholder placeholder);
        Artillery CreateArtillery(IPlaceholder placeholder);
        Barrack CreateBarrack(IPlaceholder placeholder);
    }
}