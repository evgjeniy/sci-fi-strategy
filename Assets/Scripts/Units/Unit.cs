using Dreamteck.Splines;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    private IPathFollower pathFollower;

    private void OnEnable()
    {
        //pathFollower = new SplinePathFollower(GetComponent<SplineFollower>(), GetComponent<SplineTracer>());
        pathFollower = new NavPathFollower(GetComponent<NavMeshAgent>());

    }

    [Button("Stop")]
    public void StopMovement()
    {
        pathFollower?.Stop();
    }

    [Button("Start")]
    public void StartMovement()
    {
        ((NavPathFollower)pathFollower).MoveTo(Vector3.left * 10);
        pathFollower?.Start();
    }
}
