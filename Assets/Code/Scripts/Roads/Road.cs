using System.Collections;
using System.Collections.Generic;
using SustainTheStrain.Roads;
using UnityEngine;

namespace SustainTheStrain.Roads
{
    public abstract class Road : MonoBehaviour, IRoad
    {
        public abstract Vector3 Project(Vector3 position);
    }
}
