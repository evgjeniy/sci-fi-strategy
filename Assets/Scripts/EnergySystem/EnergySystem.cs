using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Systems
{
    public class EnergySystem : System
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override bool TrySpend(int value)
        {
            return base.TrySpend(value);
            Debug.Log("Energy spended tra-la-la");
        }
        
        public override bool TryRefill(int value)
        {
            return base.TryRefill(value);
            //CurrentCount += value;
        }
    }
}

