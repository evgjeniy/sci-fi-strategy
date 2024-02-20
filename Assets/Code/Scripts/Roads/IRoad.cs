using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Roads
{
    public interface IRoad
    {
        public Vector3 Project(Vector3 position);
    }
}
