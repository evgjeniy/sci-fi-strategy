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

        public override void Spend(int value)
        {
            base.Spend(value);
            Debug.Log("Energy spended tra-la-la");
        }
    }
}

