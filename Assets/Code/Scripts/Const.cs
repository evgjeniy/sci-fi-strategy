namespace SustainTheStrain
{
    public static class Const
    {
        public const float RotationSpeed = 180.0f; // Degrees Per Seconds
        public static Observable<bool> IsDebugRadius { get; private set; } = new(false);
        public static bool FirstGameStarted { get; set; } = true;

        public static class ResourcePath
        {
            public static class Buildings
            {
                public const string Root = nameof(Buildings);

                public static class Configs
                {
                    public const string Root = Buildings.Root + "/" + nameof(Configs);

                    public const string Rocket = Root + "/" + nameof(Rocket);
                    public const string Laser = Root + "/" + nameof(Laser);
                    public const string Artillery = Root + "/" + nameof(Artillery);
                    public const string Barrack = Root + "/" + nameof(Barrack);
                }

                public static class Prefabs
                {
                    public const string Root = Buildings.Root + "/" + nameof(Prefabs);

                    public const string BuildingSelectorMenu = Root + "/UI/" + nameof(BuildingSelectorMenu);

                    public const string Rocket = Root + "/" + nameof(Rocket);
                    public const string RocketRadiusVisualizer = Root + "/" + nameof(RocketRadiusVisualizer);
                    public const string RocketManagementMenu = Root + "/UI/" + nameof(RocketManagementMenu);

                    public const string Laser = Root + "/" + nameof(Laser);
                    public const string LaserRadiusVisualizer = Root + "/" + nameof(LaserRadiusVisualizer);
                    public const string LaserManagementMenu = Root + "/UI/" + nameof(LaserManagementMenu);

                    public const string Artillery = Root + "/" + nameof(Artillery);
                    public const string ArtilleryRadiusVisualizer = Root + "/" + nameof(ArtilleryRadiusVisualizer);
                    public const string ArtilleryManagementMenu = Root + "/UI/" + nameof(ArtilleryManagementMenu);

                    public const string Barrack = Root + "/" + nameof(Barrack);
                    public const string BarrackRadiusVisualizer = Root + "/" + nameof(BarrackRadiusVisualizer);
                    public const string BarrackManagementMenu = Root + "/UI/" + nameof(BarrackManagementMenu);
                }
            }

            public static class Abilities
            {
                public const string Root = nameof(Abilities);

                public static class Configs
                {
                    public const string Root = Abilities.Root + "/" + nameof(Configs);
                    
                    
                }

                public static class Prefabs
                {
                    public const string Root = Abilities.Root + "/" + nameof(Prefabs);
                }
            }

            public static class EnergySystems
            {
                public const string Root = nameof(EnergySystems);

                public static class Configs
                {
                    public const string Root = EnergySystems.Root + "/" + nameof(Configs);
                }
            }

            public static class Units
            {
                public const string Root = nameof(Units);

                public static class Configs
                {
                    public const string Root = Units.Root + "/" + nameof(Units);
                }
            }
        }

        public static class Order
        {
            private const int Root = 0;
            private const int DivideValue = 11;

            public const int EnergyConfigs = Root + DivideValue;
            public const int UnitConfigs = EnergyConfigs + DivideValue;
            public const int BuildingConfigs = UnitConfigs + DivideValue;
            public const int AbilityConfigs = BuildingConfigs + DivideValue;
        }
    }
}