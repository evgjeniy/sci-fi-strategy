using Zenject;

namespace SustainTheStrain.Installers
{
    public class TimerFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Timer>().FromFactory<TimerFactory>().AsTransient();
        }

        public class TimerFactory : IFactory<Timer>
        {
            private readonly TickableManager _tickableManager;

            public TimerFactory(TickableManager tickableManager) => _tickableManager = tickableManager;

            public Timer Create()
            {
                var timer = new Timer();
                _tickableManager.Add(timer);

                return timer;
            }
        }
    }
}