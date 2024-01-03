using Leopotam.Ecs;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class TestTransformSystem : IEcsInitSystem
{
    private readonly EcsFilter<TransformReference> _transformFilter;

    public void Init()
    {
        foreach (var entityId in _transformFilter)
        {
            ref var transformReference = ref _transformFilter.Get1(entityId);
            var transform = transformReference.Transform;

            transform.DOMove(transform.up, 1.0f).SetLoops(-1, LoopType.Yoyo).SetLink(transform.gameObject).Play();
        }
    }
}