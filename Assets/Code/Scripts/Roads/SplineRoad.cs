using Dreamteck.Splines;
using UnityEngine;

namespace SustainTheStrain.Roads
{
    [RequireComponent(typeof(SplineComputer))]
    public class SplineRoad : Road
    {
        private SplineComputer _splineComputer;
        
        private void Awake()
        {
            _splineComputer = GetComponent<SplineComputer>();
        }
        
        public override Vector3 Project(Vector3 pos)
        {
            if (_splineComputer == null) return Vector3.zero;
            
            var splineSample = _splineComputer.Project(pos, 0, 1);
            return splineSample.position;
        }
    }
}
