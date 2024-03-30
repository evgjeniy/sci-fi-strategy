namespace SustainTheStrain.Units
{
    public interface ITeam
    {
        public Team Team { get; set; }
    }
    
    public enum Team { Enemy, Player }
}
