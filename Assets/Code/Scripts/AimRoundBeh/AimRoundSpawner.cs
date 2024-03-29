using UnityEngine;
using UnityEngine.Extensions;

namespace SustainTheStrain
{
    public class AimSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject decalPrefab;

        public void SpawnAimRound(Vector3 point)
        {
            Instantiate(decalPrefab, point, Quaternion.Euler(90, 0, 0));
        }
    }
}
