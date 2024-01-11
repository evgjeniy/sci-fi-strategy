using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SplinePathFollower : IPathFollower
{
    private readonly SplineFollower splineFollower;
    private readonly SplineTracer splineTracer;

    public SplinePathFollower(SplineFollower splineFollower, SplineTracer splineTracer)
    {
        this.splineFollower = splineFollower;
        this.splineTracer = splineTracer;

        splineTracer.onNode += OnNode;
    }

    private void OnNode(List<SplineTracer.NodeConnection> passed)
    {
        UnityEngine.Debug.Log("Reached node " + passed[0].node.name + " connected at point " + passed[0].point);
        Node.Connection[] connections = passed[0].node.GetConnections();
        if (connections.Length == 1) return;
        int newConnection = UnityEngine.Random.Range(0, connections.Length);
        if (connections[newConnection].spline == splineTracer.spline &&
        connections[newConnection].pointIndex == passed[0].point)
        {
            newConnection++;
            if (newConnection >= connections.Length) newConnection = 0;
        }
        SwitchSpline(connections[newConnection]);
    }

    private void SwitchSpline(Node.Connection to)
    {
        splineTracer.spline = to.spline;

        splineTracer.RebuildImmediate();

        double startpercent = splineTracer.ClipPercent(to.spline.GetPointPercent(to.pointIndex));
        splineTracer.SetPercent(startpercent+0.01);
    }

    public void Stop()
    {
        splineFollower.follow = false;
    }

    public void Start()
    {
        splineFollower.follow = true;
    }
}
