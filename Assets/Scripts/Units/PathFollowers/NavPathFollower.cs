using UnityEngine;
using UnityEngine.AI;

public class NavPathFollower : IPathFollower
{
    private readonly NavMeshAgent agent;

    public float Speed { get => agent.speed; set => agent.speed = value; }

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

    public void Start()
    {
        agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;
    }
}
