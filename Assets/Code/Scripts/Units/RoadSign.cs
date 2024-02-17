using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

namespace SustainTheStrain.Units
{
    [RequireComponent(typeof(Node))]
    public class RoadSign : MonoBehaviour
    {
        [field:SerializeField] [field:HideInInspector]
        public bool [] Guides { get; set; }
    }
}
