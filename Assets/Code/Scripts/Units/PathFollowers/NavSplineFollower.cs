using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;

namespace SustainTheStrain
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavSplineFollower : SplineTracer
    {
        private NavMeshAgent _navMeshAgent;
        private Coroutine _movingRoutine;
        public Vector3 CurrentDestination { get; set; }
        
        [SerializeField]
        [HideInInspector]
        private float _followSpeed = 1f;
        
        
        [SerializeField]
        [HideInInspector]
        [UnityEngine.Serialization.FormerlySerializedAs("follow")]
        private bool _follow = true;
        
        [HideInInspector]
        public bool autoStartPosition = false;
        public float followSpeed
        {
            get { return _navMeshAgent.speed; }
            set
            {
                if (_followSpeed != value)
                {
                    _followSpeed = value;
                    Spline.Direction lastDirection = _direction;
                    if (Mathf.Approximately(_followSpeed, 0f)) return;
                    if (_followSpeed < 0f)
                    {
                        direction = Spline.Direction.Backward;
                    }
                    if(_followSpeed > 0f)
                    {
                        direction = Spline.Direction.Forward;
                    }
                }
            }
        }

        public bool follow
        {
            get { return _follow; }
            set
            {
                if(_follow != value)
                {
                    if (autoStartPosition)
                    {
                        Project(GetTransform().position, ref evalResult);
                        SetPercent(evalResult.percent);
                    }
                    _follow = value;
                }
            }
        }
        
        void Move()
        {
            _movingRoutine = StartCoroutine(Moving());
        }

        protected override void OnEnable()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            Move();
        }

        private IEnumerator Moving()
        {
            int i=0, inc=1, target=spline.sampleCount;
            for (; i != target; i += inc)
            {
                NavMesh.SamplePosition(spline.rawSamples[i].position, out var hit, 100f, 1);
                _navMeshAgent.SetDestination(hit.position);
                yield return new WaitUntil(()=>_navMeshAgent.velocity.sqrMagnitude < 0.1);
            }
            
            yield return new WaitUntil(() => _navMeshAgent.velocity.sqrMagnitude < 0.1);
            InvokeNodes();
        }
        
    }
}
