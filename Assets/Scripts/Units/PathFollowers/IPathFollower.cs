using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathFollower
{
    public float Speed { get; set; }

    public void Start();
    public void Stop();
}
