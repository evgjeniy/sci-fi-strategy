using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

public sealed class EcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _updateSystems;

    private void Start()
    {
        _world = new EcsWorld();
        _updateSystems = new EcsSystems(_world).ConvertScene();

        AddSystems();
        AddOneFrames();

        _updateSystems.Init();
    }

    private void AddSystems()
    {
        _updateSystems.Add(new TestTransformSystem());
    }

    private void AddOneFrames()
    {
        // Компонент, живущий в рамках одного кадра (используются для ивентов, запросов)
        // _updateSystems.OneFrame<SomeComponent>();
    }

    private void Update() => _updateSystems?.Run();

    private void Destroy()
    {
        _updateSystems?.Destroy();
        _world?.Destroy();

        _updateSystems = null;
        _world = null;
    }
}