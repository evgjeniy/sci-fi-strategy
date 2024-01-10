namespace Ecs.Components
{
    public class NavMeshAgentProvider : Voody.UniLeo.MonoProvider<NavMeshAgentRef> {}

    [System.Serializable]
    public struct NavMeshAgentRef
    {
        public UnityEngine.AI.NavMeshAgent agent;
    }
}