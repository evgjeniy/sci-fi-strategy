using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    public interface IZoneVisualizer
    {
        public float Radius { get; set; }
        public float Angle { get; set;}
        public float Direction { get; set; }
    }
}
