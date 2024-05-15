using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Citadels
{
    [RequireComponent(typeof(Damageble))]
    public class Citadel : MonoBehaviour
    {
        public Damageble damageble;

        private void Awake()
        {
            damageble = GetComponent<Damageble>();
        }
    }
}
