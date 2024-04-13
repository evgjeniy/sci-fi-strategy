using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SustainTheStrain
{
    public class ZoneVisualizer : DecalProjector, IZoneVisualizer
    {
        private static readonly int AngleMaterialNameID = Shader.PropertyToID("_Angle");
        private const float ScaleFactor = 1.1f;

        public float Radius
        {
            get => size.x / (ScaleFactor * 2);
            set => size = new Vector3(value * ScaleFactor * 2, value * ScaleFactor * 2, size.z);
        }

        public float Angle
        {
            get => material.GetFloat(AngleMaterialNameID);
            set => material.SetFloat(AngleMaterialNameID, Mathf.Clamp(value, 0f, 360f));
        }

        public float Direction
        {
            get => transform.rotation.y;
            set => transform.rotation = Quaternion.Euler(90, value, 0);
        }

        protected virtual void Awake() => material = new Material(material);
    }
}
