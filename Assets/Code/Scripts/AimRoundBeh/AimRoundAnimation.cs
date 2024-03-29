using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain
{
    public class AimRoundAnimation : MonoBehaviour
    {
        [SerializeField] private float startRadius;
        [SerializeField] private float endRadius;
        [SerializeField] private float growSpeed;
        [SerializeField] private float decreaseSpeed;
        [SerializeField] private float delayTime;
        private ZoneVisualizer zoneVisualizer;

        delegate void CurStateDelegate();
        CurStateDelegate curState;

        private float delayStart;

        private void GrowingLogic()
        {
            if (zoneVisualizer.Radius < endRadius)
                zoneVisualizer.Radius += growSpeed * Time.deltaTime;
            else
            {
                curState = DelayingLogic;
                delayStart = Time.time;
            }
        }

        private void DecreasingLogic()
        {
            if (zoneVisualizer.Radius <= startRadius)
                this.DestroyObject();
            else
                zoneVisualizer.Radius -= decreaseSpeed * Time.deltaTime;
        }

        private void DelayingLogic()
        {
            if (Time.time - delayStart > delayTime)
                curState = DecreasingLogic;
        }

        void Start()
        {
            zoneVisualizer = GetComponent<ZoneVisualizer>();
            zoneVisualizer.Radius = startRadius;
            curState = GrowingLogic;
        }

        void Update()
        {
            curState();
        }
    }
}
