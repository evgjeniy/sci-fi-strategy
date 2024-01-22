using System.Collections.Generic;
using Dreamteck.Splines;

namespace SustainTheStrain.Units.PathFollowers
{
    public class SplinePathFollower : IPathFollower
    {
        private readonly SplineFollower follower;

        public float Speed { get => follower.followSpeed; set => follower.followSpeed = value; }

        public SplinePathFollower(SplineFollower splineFollower)
        {
            follower = splineFollower;

            follower.onNode += OnNode;
        }

        private void OnNode(List<SplineTracer.NodeConnection> passed)
        {
            UnityEngine.Debug.Log("Reached node " + passed[0].node.name + " connected at point " + passed[0].point);
            Node.Connection[] connections = passed[0].node.GetConnections();
            if (connections.Length == 1) return;
            int newConnection = UnityEngine.Random.Range(0, connections.Length);
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
            follower.SetPercent(startpercent+0.01);
        }

        public void Stop()
        {
            follower.enabled = false;
            follower.follow = false;
        }

        public void Start()
        {
            follower.enabled = true;
            follower.follow = true;
        }
    }
}
