using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class ChainUpdate : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private float lifeTime;
        private List<Collider> colliders;
        private float startTime;

        public void setFields(float time, List<Collider> colls)
        {
            lifeTime = time;
            colliders = colls;
        }

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            startTime = Time.time;
        }

        void Update()
        {
            if (Time.time - startTime > lifeTime)
            {
                End();
                return;
            }
            if (lineRenderer == null || colliders == null) return;
            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i] == null)
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(getIdx(colliders.Count, i)));
                else
                    lineRenderer.SetPosition(i, colliders[i].transform.position);
            }
        }

        private int getIdx(int siz, int arg)
        {
            if (siz == 1)
                return 0;
            if (arg == 0)
                return 1;
            return arg - 1;
        }

        private void End()
        {
            colliders = null;
            Destroy(lineRenderer);
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
