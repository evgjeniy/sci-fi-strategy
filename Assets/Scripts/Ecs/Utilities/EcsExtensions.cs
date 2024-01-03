using UnityEngine;
using Leopotam.Ecs;

public static class EcsExtensions
{
    public static bool HasEntity<T>(this EcsWorld world) where T : struct
    {
        var ecsFilter = world.GetFilter(typeof(EcsFilter<T>));
        return !ecsFilter.IsEmpty();
    }
        
    public static void SendMessage<T>(this EcsWorld world, in T message = default) where T : struct
    {
        world.NewEntity().Get<T>() = message;
    }
        
    public static void SendMessage<T>(this ref EcsEntity entity, in T message = default) where T : struct
    {
        entity.Get<T>() = message;
    }

    public static ref T GetComponent<T>(this EcsWorld world, in int index = 0) where T : struct
    {
        return ref world.GetEntity<T>(index).Get<T>();
    }

    public static bool TryGetEntity<T>(this EcsWorld world, out EcsEntity entity, in int index = 0) where T : struct
    {
        var ecsFilter = world.GetFilter(typeof(EcsFilter<T>));

        if (index < 0 || index >= ecsFilter.GetEntitiesCount())
        {
            entity = default;
            return false;
        }

        entity = ecsFilter.GetEntity(index);
        return true;
    }

    public static ref EcsEntity GetEntity<T>(this EcsWorld world, in int index = 0) where T : struct
    {
        var ecsFilter = world.GetFilter(typeof(EcsFilter<T>));

        if (index < 0 || index >= ecsFilter.GetEntitiesCount())
            Debug.LogWarning($"There's no entity by index {index} with tag {typeof(T).Name}");

        return ref ecsFilter.GetEntity(index);
    }

    public static void AddComponentByTag<T1, T2>(this EcsWorld world, in T2 message = default) 
        where T1 : struct
        where T2 : struct
    {
        var ecsFilter = world.GetFilter(typeof(EcsFilter<T1>));
        if (ecsFilter.IsEmpty())
        {
            Debug.LogWarning($"There's no entities with tag: {typeof(T1).Name}");
            return;
        }

        foreach (var entityId in ecsFilter)
            ecsFilter.GetEntity(entityId).Get<T2>() = message;
    }

    public static void AddComponentByTag<T1, T2>(this EcsWorld world, int index, in T2 message = default)
        where T1 : struct
        where T2 : struct
    {
        var ecsFilter = world.GetFilter(typeof(EcsFilter<T1>));
        
        if (ecsFilter.IsEmpty())
            Debug.LogWarning($"There's no entities with tag: {typeof(T1).Name}");
        else if (index < 0 || index >= ecsFilter.GetEntitiesCount())
            Debug.LogWarning($"There's no entity by index {index} with tag {typeof(T1).Name}");
        else 
            ecsFilter.GetEntity(index).Get<T2>() = message;
    }
}