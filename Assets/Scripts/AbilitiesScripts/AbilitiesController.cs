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
        [SerializeField] private int zoneDamageMaxDistFromCamera;
        [SerializeField] private float zoneDamageRadius;
        [SerializeField] private float zoneDamageReloadingSpeed;
        [SerializeField] private float damag;
        [SerializeField] private float speedCoef;

        [SerializeField] private int teamOpponent;

        private ZoneAim zoneAim;

        public readonly List<BaseAbility> Abilities = new(); //better to make it readonly

        private ReloadDelegate[] _reloadList; //tut private, dlya dobavlenia est metod
        private int _selected = -1;

        public delegate void ReloadDelegate(int idx, float l, bool r);

        public void Init() //temporary, because now we don't have MainController
        {
            AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed, damag));
            AddAbility(new ZoneSlownessAbility(zoneDamageRadius, zoneDamageReloadingSpeed, speedCoef));
            AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed, damag));
            ReloadListSyncSize(); //êîãäà âñå àáèëêè äîáàâëåíû
            zoneAim = new(zoneDamageRadius, aimZonePrefab, layersToHit, zoneDamageMaxDistFromCamera); 
        }

        public void setTeamOpponent(int team) => teamOpponent = team;

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

            zoneAim.Destroy();
            var sel = _selected;
            _selected = -1;
            
            return sel == type;
        }

        public void OnAbilitySelect(int idx)
        {
            if (IsCurrentSelected(idx)) return;
            
            _selected = idx; //!!! need to swap AIM
            if (Abilities[_selected] is ZoneAbility zoneAbility)
                zoneAim.SpawnAimZone();
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

            var hit = zoneAim.GetAimInfo(ray);
            if (hit.HasValue)
            {
                zoneAim.UpdateLogic(hit.Value.point);
                if (UnityEngine.Input.GetMouseButtonDown(0))
                    Abilities[_selected].Shoot(hit.Value, teamOpponent);
            }
        }
    }
}
