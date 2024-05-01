using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public class AdditionalBarrackRecruitEffect : MonoBehaviour
    {
        private Barrack _barrack;
        private int _recruitsAmount;

        public void Initialize(int recruitsAmount)
        {
            if (_barrack != null) return;
            
            if (TryGetComponent(out _barrack) is false)
            {
                Destroy(this);
                return;
            }
            
            _recruitsAmount = recruitsAmount;
            _barrack.RecruitGroup.squadMaxSize += _recruitsAmount;
        }

        private void OnDestroy() => _barrack.RecruitGroup.squadMaxSize -= _recruitsAmount;
    }
}