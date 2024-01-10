using Leopotam.Ecs;
using Ecs.Components;
using Ecs.Components.Requests;

namespace Ecs.Systems
{
    public class DestinationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<NavMeshAgentRef, SetDestinationRequest> _agentDestinationFilter;
        
        public void Run()
        {
            foreach (var entityId in _agentDestinationFilter)
            {
                ref var entity = ref _agentDestinationFilter.GetEntity(entityId);
                ref var navMeshAgent = ref entity.Get<NavMeshAgentRef>().agent;
                ref var destination = ref entity.Get<SetDestinationRequest>().destination;

                navMeshAgent.SetDestination(destination);
                entity.Del<SetDestinationRequest>();
            }
        }
    }
}