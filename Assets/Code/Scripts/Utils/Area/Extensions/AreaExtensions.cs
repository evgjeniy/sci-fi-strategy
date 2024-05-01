using System.Linq;

namespace SustainTheStrain
{
    public static class AreaExtensions
    {
        public static bool IsIn<TComponent>(this TComponent component, Area<TComponent> area) => area.Entities.Contains(component);
        public static bool IsNotIn<TComponent>(this TComponent component, Area<TComponent> area) => !area.Entities.Contains(component);
    }
}