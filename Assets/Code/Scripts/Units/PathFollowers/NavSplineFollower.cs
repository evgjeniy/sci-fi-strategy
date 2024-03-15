using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace SustainTheStrain
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavSplineFollower : SplineTracer
    {
        [SerializeField] private GameObject markerPrefab;
        private NavMeshAgent _navMeshAgent;
        private Coroutine _movingRoutine = null;
        
        void Move()
        {
            if (_movingRoutine != null) return;
            _movingRoutine = StartCoroutine(Moving());
        }
        
        protected override void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif
            Move();
            //StartCoroutine(DebugMe());
        }

        private IEnumerator DebugMe()
        {
            while (true)
            {
                Debug.Log(_navMeshAgent.remainingDistance);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator Moving()
        {
            int i=0;
            var splinePoints = spline.GetPoints();
            for (; i < splinePoints.Length; i++)
            {
                NavMesh.SamplePosition(splinePoints[i].position, out var hit, 100f, 1);
                _navMeshAgent.SetDestination(hit.position);
                markerPrefab.SetActive(true);
                markerPrefab.transform.position = hit.position;
                //Debug.Log($"Distance is {_navMeshAgent.remainingDistance}");
                yield return new WaitUntil(()=>!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance );
                markerPrefab.SetActive(false);
            }
            
            yield return new WaitUntil(() => _navMeshAgent.velocity.sqrMagnitude < 0.1);
            InvokeNodes();
        }

        public override void EditorAwake()
        {
            Debug.Log("Hello");
        }
    }
}
