using SustainTheStrain.Installers;

namespace SustainTheStrain.AbilitiesNew
{
    public sealed class ZoneDamageAbility : Ability<ZoneDamageAbilityData, ZoneDamageAbilityView>
    {
        public override ZoneDamageAbilityData Model { get; }
        public override ZoneDamageAbilityView View { get; }

        public ZoneDamageAbility(AbilityController abilityController, IStaticDataService staticDataService) : base(abilityController)
        {
            Model = staticDataService.GetAbilityData<ZoneDamageAbilityData>();
            View = UnityEngine.Object.Instantiate(Model.ViewPrefab);
        }
    }
}