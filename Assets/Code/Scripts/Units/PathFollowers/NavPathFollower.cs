using UnityEngine;
using UnityEngine.AI;

namespace SustainTheStrain.Units
{
    public class NavPathFollower : IPathFollower
    {
        private readonly NavMeshAgent agent;

        public float Speed { get => agent.speed; set => agent.speed = value; }

        public bool IsStopped => agent.isStopped;
        
        public NavPathFollower(NavMeshAgent agent)
        {
            this.agent = agent;
        }
        
        public bool IsDestinationReached()
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void MoveTo(Vector3 position)
        {
            agent.destination = position;

        }

        public bool MoveToWithCheck(Vector3 position)
        {
            NavMeshPath navMeshPath = new NavMeshPath();

            if (agent.CalculatePath(position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetPath(navMeshPath);
                return true;
            }

            return false;
        }
        
        public void Start()
        {
            if (!agent.isActiveAndEnabled) return;

            agent.isStopped = false;
            agent.avoidancePriority = 100;
            //agent.Enable();
        }

        public void Stop()
        {
            if (!agent.isActiveAndEnabled) return;

            agent.isStopped = true;
            agent.avoidancePriority = 50;
            //agent.Disable();
        }
    }
}
