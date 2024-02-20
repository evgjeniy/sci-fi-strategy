using SustainTheStrain.Installers;

namespace SustainTheStrain.AbilitiesNew
{
    public sealed class ChainDamageAbility : Ability<ChainDamageAbilityData>
    {
        public override ChainDamageAbilityData Model { get; }
        public override AbilityView View { get; }
        
        public ChainDamageAbility(AbilityController abilityController, IStaticDataService staticDataService) : base(abilityController)
        {
            Model = staticDataService.GetAbilityData<ChainDamageAbilityData>();
            View = UnityEngine.Object.Instantiate(Model.ViewPrefab);
        }
    }
}