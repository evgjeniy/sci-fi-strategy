using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain
{
    public class AimRoundAnimation : MonoBehaviour
    {
        [SerializeField] private float startRadius;
        [SerializeField] private float endRadius;
        [SerializeField] private float growSpeed;
        [SerializeField] private float lifeTimeAfterGrow;
        private ZoneVisualizer zoneVisualizer;
        private bool destructStarted = false;

        void Start()
        {
            zoneVisualizer = GetComponent<ZoneVisualizer>();
            zoneVisualizer.Radius = startRadius;
        }

        void Update()
        {
            if (destructStarted) return;

            if (zoneVisualizer.Radius < endRadius)
                zoneVisualizer.Radius += growSpeed * Time.deltaTime;
            else
            {
                destructStarted = true;
                this.DestroyObject(lifeTimeAfterGrow);
            }
        }
    }
}
