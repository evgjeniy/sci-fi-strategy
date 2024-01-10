namespace Ecs.Components.Requests
{
    public class SetDestinationRequestProvider : Voody.UniLeo.MonoProvider<SetDestinationRequest> {}

    [System.Serializable]
    public struct SetDestinationRequest
    {
        public UnityEngine.Vector3 destination;
    }
}