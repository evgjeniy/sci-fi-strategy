using SustainTheStrain._Contracts.Configs.Buildings;
using SustainTheStrain.Abilities;
using SustainTheStrain.Units;

namespace SustainTheStrain._Contracts.Buildings
{
    public class BarrackData
    {
        public readonly Observable<BarrackBuildingConfig> Config;
        public readonly Outline Outline;
        public readonly RecruitGroup RecruitGroup;

        public bool IsUnitsPointState { get; set; }

        public BarrackData(BarrackBuildingConfig startConfig, Outline outline, RecruitGroup recruitGroup)
        {
            Outline = outline;
            RecruitGroup = recruitGroup;
            Config = new Observable<BarrackBuildingConfig>(startConfig);
        }
    }
}