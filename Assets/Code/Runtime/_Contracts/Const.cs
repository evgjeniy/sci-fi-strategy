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
                    public const string RocketCreateButton = Root + "/UI/" + nameof(RocketCreateButton);
                    public const string LaserCreateButton = Root + "/UI/" + nameof(LaserCreateButton);
                    public const string ArtilleryCreateButton = Root + "/UI/" + nameof(ArtilleryCreateButton);
                    public const string BarrackCreateButton = Root + "/UI/" + nameof(BarrackCreateButton);
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

            public const string Units = "Units";
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