using System;
using System.Collections;
using UnityEngine;

namespace ResourceSystems
{
    public abstract class ResourceGenerator : MonoBehaviour
    {
        public Action<int> OnResourceGenerated;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected int _generateCount;
        protected Coroutine _generatingRoutine;
        protected bool _canGenerate;
        
        public virtual void StartGeneration(){}
        
        public virtual void EndGeneration(){}

        public virtual IEnumerator GenerateResource() {return null;}

        public virtual void IncreaseGenerateCount(){}
        
        public virtual void IncreaseGenerateSpeed(){}
        
    }
}