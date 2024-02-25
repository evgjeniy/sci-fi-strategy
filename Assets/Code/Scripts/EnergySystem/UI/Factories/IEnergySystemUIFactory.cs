namespace SustainTheStrain.EnergySystem.UI.Factories
{
    public interface IEnergySystemUIFactory
    {
        public EnergySystemUI Create(IEnergySystem system);
    }
}