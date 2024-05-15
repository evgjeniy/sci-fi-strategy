using UnityEngine;

namespace SustainTheStrain.Tips
{
    public class DeactivateOnStart : MonoBehaviour
    {
        private enum DeactivateType { None, OnAwake, OnEnable, OnStart }

        [SerializeField] private DeactivateType _deactivateType = DeactivateType.OnStart;

        private void Awake()
        {
            if (_deactivateType == DeactivateType.OnAwake)
                gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            if (_deactivateType == DeactivateType.OnEnable)
                gameObject.SetActive(false);
        }
        
        private void Start()
        {
            if (_deactivateType == DeactivateType.OnStart)
                gameObject.SetActive(false);
        }
    }
}
