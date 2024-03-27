using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units
{
    public class NavSplinePathFollower : IPathFollower
    {
        private readonly NavSplineFollower follower;

        public float Speed { get => follower.NavMeshAgent.speed; set => follower.NavMeshAgent.speed = value; }

        public NavSplinePathFollower(NavSplineFollower splineFollower)
        {
            follower = splineFollower;

            follower.onNode += OnNode;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OnNode(List<SplineTracer.NodeConnection> passed)
        {
            UnityEngine.Debug.Log("Reached node " + passed[0].node.name + " connected at point " + passed[0].point);
            Node.Connection[] connections = passed[0].node.GetConnections();
            if (connections.Length == 1) return;

            if(!passed[0].node.TryGetComponent<RoadSign>(out var roadSign)) return;

            int newConnection = 0;
            
            for (int i = 0; i < roadSign.Guides.Length; i++)
            {
                if (roadSign.Guides[i])
                {
                    newConnection = i; 
                    break;
                }
            }

            if (connections[newConnection].spline == follower.spline &&
                connections[newConnection].pointIndex == passed[0].point)
            {
                newConnection++;
                if (newConnection >= connections.Length) newConnection = 0;
            }
            SwitchSpline(connections[newConnection]);
        }

        private void SwitchSpline(Node.Connection to)
        {
            follower.spline = to.spline;
            follower.MovingPercent = 0;
            follower.RebuildImmediate();
            follower.Move();
        }

        public void Stop()
        {
            follower.Disable();
            follower.NavMeshAgent.isStopped = true;
            follower.CancelMoving();
        }

        public void Start()
        {
            follower.Enable();
            follower.NavMeshAgent.isStopped = false;
            follower.Move();
        }
        
    }
}
