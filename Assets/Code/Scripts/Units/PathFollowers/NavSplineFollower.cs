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
        //[SerializeField] private GameObject markerPrefab;
        [SerializeField] private float _splineStep;
        private NavMeshAgent _navMeshAgent;
        private Coroutine _movingRoutine = null;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        
        public void Move()
        {
            //if (_movingRoutine != null) return;
            _movingRoutine = StartCoroutine(Moving());
        }
        
//        protected override void Start()
//        {
//#if UNITY_EDITOR
//            if (!Application.isPlaying) return;
//#endif
            
//            //StartCoroutine(DebugMe());
//        }
        
        public bool IsDestinationReached()
        {
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
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
            float i = 0;
            for (; i <= 1; i+=_splineStep)
            {
                var nextStep = spline.Evaluate(i);
                NavMesh.SamplePosition(nextStep.position, out var hit, 100f, 1);
                _navMeshAgent.SetDestination(hit.position);
                yield return new WaitUntil(IsDestinationReached);
                
            }
            CheckNodes(0, 1);
            //CheckTriggers(_splineStep, 1);
            yield return new WaitUntil(IsDestinationReached);
            InvokeNodes();
        }

        public override void EditorAwake()
        {
            Debug.Log("Hello");
        }

        protected override void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
    }
}
