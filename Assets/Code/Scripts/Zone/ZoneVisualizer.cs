using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain
{
    [RequireComponent(typeof(DecalProjector))]
    public class ZoneVisualizer : MonoBehaviour, IZoneVisualizer
    {
        [SerializeField] [Range(0f, 360f)] private float _angle = 90;
        [SerializeField] [Min(0)] private float _radius = 5;

        private DecalProjector _projector;
        private float _projectionDepth = 50;
        private float _scaleFactor = 1.1f;

        public float Radius 
        { 
            get => _radius; 
            set { _radius = value; UpdateRadius(); }
        }

        public float Angle 
        { 
            get => _angle;
            set { _angle = Mathf.Clamp(value, 0f, 360f); UpdateMaterial(); }
        }

        public float Direction 
        { 
            get => transform.rotation.y;
            set => transform.rotation = Quaternion.Euler(90, value, 0); 
        }

        private void Awake()
        {
            _projector = GetComponent<DecalProjector>();

            _projector.material = new Material(_projector.material);
        }

        private void OnEnable()
        {
            UpdateMaterial();
            UpdateRadius();
        }

        public void UpdateMaterial()
        {
            _projector.material.SetFloat("_Angle", _angle);
        }

        public void UpdateRadius()
        {
            _projector.size = new Vector3(_radius * _scaleFactor * 2, _radius * _scaleFactor * 2, _projectionDepth);
        }
    }
}
