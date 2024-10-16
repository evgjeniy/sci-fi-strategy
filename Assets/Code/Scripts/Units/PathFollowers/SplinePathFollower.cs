using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine.Extensions;

namespace SustainTheStrain.Units
{
    public class SplinePathFollower : IPathFollower
    {
        private readonly SplineFollower follower;
        
        public event Action OnNavigatePointChanged;

        public float Speed { get => follower.followSpeed; set => follower.followSpeed = value; }

        public SplinePathFollower(SplineFollower splineFollower)
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

            follower.RebuildImmediate();
            double startpercent = follower.ClipPercent(to.spline.GetPointPercent(to.pointIndex));
            var sample = to.spline.EvaluatePosition(startpercent);
            follower.SetPercent(startpercent+0.00001);
        }

        public void Stop()
        {
            follower.Disable();
            follower.follow = false;
        }

        public void Start()
        {
            follower.Enable();
            follower.SetPercent(follower.spline.Project(follower.transform.position).percent);
            follower.follow = true;
        }
    }
}
