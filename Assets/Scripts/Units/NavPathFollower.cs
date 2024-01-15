using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPathFollower : IPathFollower
{
    private readonly NavMeshAgent agent;

    public NavPathFollower(NavMeshAgent agent)
    {
        this.agent = agent;
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
