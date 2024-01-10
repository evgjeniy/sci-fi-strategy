using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;
using Ecs.Systems;

namespace Ecs
{
    public sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _updateSystems;

        private void Start()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world).ConvertScene();

            _updateSystems.Add(new HeroInputSystem());
            _updateSystems.Add(new DestinationSystem());
            _updateSystems.Init();
        }

        private void Update() => _updateSystems?.Run();

        private void OnDestroy()
        {
            _updateSystems?.Destroy();
            _world?.Destroy();

            _updateSystems = null;
            _world = null;
        }
    }
}