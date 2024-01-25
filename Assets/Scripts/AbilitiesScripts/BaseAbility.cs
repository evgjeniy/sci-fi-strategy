using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public abstract class BaseAbility
    {
        private const float Eps = 0.00001f;
        protected float Reload = 1f;
        protected float LoadingSpeed;

        public float GetReload() => Mathf.Abs(Reload - 1f) < Eps ? 1f : Mathf.Min(Reload, 1f);

        public void SetLoadingSpeed(float speed) => LoadingSpeed = speed; //ne znayu nuzhen li no pust budet))

        public void Shoot(RaycastHit hit)
        {
            if (!IsReloaded())
            {
                FailShootLogic();
                return;
            }
            Reload = 0;
            SuccessShootLogic(hit);
        }

        public void Load(float delt)
        {
            if (IsReloaded()) return;
            
            Reload += LoadingSpeed * delt;
            if(IsReloaded())
                ReadyToShoot();
        }

        public bool IsReloaded() => GetReload() == 1f; //reload > 1 and pogreshnost ychtena v getReload()

        protected abstract void FailShootLogic();

        protected abstract void SuccessShootLogic(RaycastHit hit);

        protected abstract void ReadyToShoot();

        public abstract void UpdateLogic(RaycastHit hit);

        public abstract void DestroyLogic();
    }
}
