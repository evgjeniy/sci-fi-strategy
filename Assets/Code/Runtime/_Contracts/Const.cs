namespace SustainTheStrain._Contracts
{
    public static class Const
    {
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

                    public const string Rocket = Root + "/" + nameof(Rocket);
                    public const string Laser = Root + "/" + nameof(Laser);
                    public const string Artillery = Root + "/" + nameof(Artillery);
                    public const string Barrack = Root + "/" + nameof(Barrack);

                    public const string BuildingCreateMenu = Root + "/UI/" + nameof(BuildingCreateMenu);
                    public const string RocketView = Root + "/UI/" + nameof(RocketView);
                    public const string LaserView = Root + "/UI/" + nameof(LaserView);
                    public const string ArtilleryView = Root + "/UI/" + nameof(ArtilleryView);
                    public const string BarrackView = Root + "/UI/" + nameof(BarrackView);
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