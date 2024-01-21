using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class AbilitiesController : MonoBehaviour
    {
        [SerializeField] private GameObject aimZonePrefab;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layersToHit;
        [SerializeField] private float zoneDamageMaxDistFromCamera;
        [SerializeField] private float zoneDamageRadius;
        [SerializeField] private float zoneDamageReloadingSpeed;

        public readonly List<BaseAbility> Abilities = new(); //better to make it readonly
        private readonly Vector3 _nullVector = new(0, 0, 0);

        private ReloadDelegate[] _reloadList; //tut private, dlya dobavlenia est metod
        private GameObject _aimZone;
        private int _selected = -1;

        public delegate void ReloadDelegate(int idx, float l, bool r);

        public void Init() //temporary, because now we don't have MainController
        {
            AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed));
            AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed));
            AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed));
            ReloadListSyncSize(); //êîãäà âñå àáèëêè äîáàâëåíû
        }

        public void ReloadListSyncSize()
        {
            _reloadList = new ReloadDelegate[Abilities.Count];
        }

        public void ReloadListAdd(int idx, ReloadDelegate rd)
        {
            if (idx < 0 || idx >= _reloadList.Length)
                throw new IndexOutOfRangeException("Incorrect idx");
            _reloadList[idx] = rd;
        }

        public void AddAbility(BaseAbility ability)
        {
            Abilities.Add(ability);
        }

        public void ResetAbilities()
        {
            _reloadList = null;
            Abilities.Clear();
        }

        private bool IsCurrentSelected(int type)
        {
            if (_selected == -1) return false;
            
            Abilities[_selected].DestroyLogic();
            var sel = _selected;
            _selected = -1;
            
            return sel == type;
        }

        public void OnAbilitySelect(int idx)
        {
            if (IsCurrentSelected(idx)) return;
            
            _selected = idx;
            if (Abilities[_selected] is ZoneAbility zoneAbility)
                zoneAbility.SetAimZone(Instantiate(aimZonePrefab, _nullVector, Quaternion.Euler(90, 0, 0)));
        }

        private void Update()
        {
            for (var i = 0; i < Abilities.Count; i++)
            {
                if (Abilities[i].IsReloaded()) continue;
                
                Abilities[i].Load(Time.deltaTime);
                _reloadList[i].Invoke(i, Abilities[i].GetReload(), Abilities[i].IsReloaded());
            }

            if (_selected == -1) return;
            
            var mousePosition = UnityEngine.Input.mousePosition;
            var ray = mainCamera.ScreenPointToRay(mousePosition);
            
            if (Physics.Raycast(ray, out var hit, zoneDamageMaxDistFromCamera, layersToHit))
                Abilities[_selected].UpdateLogic(hit);
        }
    }
}
