using UnityEngine;

namespace SustainTheStrain.AbilitiesNew
{
    public abstract class AbilityData : ScriptableObject
    {
        [field: SerializeField, Min(1.0f)] public float ReloadingSpeed { get; private set; }
        [field: SerializeField, Min(0.0f)] public float ReloadCooldown { get; private set; }
        
        private float _currentReload;

        public float CurrentReload
        {
            get => _currentReload;
            set
            {
                var newReload = Mathf.Clamp(value, 0.0f, ReloadCooldown);
                if (newReload != _currentReload) return;
                _currentReload = newReload;
                CurrentReloadChanged(_currentReload);
            }
        }

        public bool IsReloaded => CurrentReload == ReloadCooldown;

        protected virtual void CurrentReloadChanged(float _) {}
    }
}