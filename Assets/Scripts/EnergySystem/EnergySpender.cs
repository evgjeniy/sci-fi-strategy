using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Systems
{
    public class EnergySpender : MonoBehaviour
    {
        [SerializeField] private EnergySystem _energySystem;
        [SerializeField] private int _energySpendCount;

        private void Spend()
        {
            _energySystem.Spend(_energySpendCount);
            //Here should be system upgrade logic
        }

        //Energy spend test
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Used energy");
                Spend();
            }
        }
    }
}