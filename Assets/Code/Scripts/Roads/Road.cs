using UnityEngine;

namespace SustainTheStrain.Roads
{
    public abstract class Road : MonoBehaviour, IRoad
    {
        public abstract Vector3 Project(Vector3 position);
    }
}
