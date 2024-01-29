using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class AbilitiesController : MonoBehaviour
    {
        [SerializeField] private GameObject aimZonePrefab;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private int maxDistFromCamera;
        [SerializeField] private float zoneRadius;
        [SerializeField] private float reloadingSpeed;
        [SerializeField] private float damag;
        [SerializeField] private float speedCoef;
        [SerializeField] private GameObject LinePrefab;

        [SerializeField] private int team;

        private BaseAim currentAim;

        public readonly List<BaseAbility> Abilities = new(); //better to make it readonly

        private ReloadDelegate[] _reloadList; //tut private, dlya dobavlenia est metod
        private int _selected = -1;

        public delegate void ReloadDelegate(int idx, float l, bool r);

        public void Init() //temporary, because now we don't have MainController
        {
            AddAbility(new ZoneDamageAbility(zoneRadius, reloadingSpeed, damag));
            AddAbility(new ZoneSlownessAbility(zoneRadius, reloadingSpeed, speedCoef, 2));
            AddAbility(new ChainDamageAbility(LinePrefab, reloadingSpeed, damag, 4, 100));
            AddAbility(new EnemyHackAbility(reloadingSpeed));
            AddAbility(new LandingAbility(reloadingSpeed, 3));
            ReloadListSyncSize(); //êîãäà âñå àáèëêè äîáàâëåíû
        }

        public void setTeamOpponent(int team) => this.team = team;

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

            currentAim.Destroy();
            currentAim = null;
            var sel = _selected;
            _selected = -1;
            
            return sel == type;
        }

        public void OnAbilitySelect(int idx)
        {
            if (IsCurrentSelected(idx)) return;
            
            _selected = idx; //!!! need to swap AIM
            if (Abilities[_selected] is ZoneAbility)
                currentAim = new ZoneAim(zoneRadius, aimZonePrefab, groundLayers, maxDistFromCamera);
            else if (Abilities[_selected] is LandingAbility)
                currentAim = new PointAim(groundLayers, maxDistFromCamera);
            else
                currentAim = new PointAim(enemyLayers, maxDistFromCamera);
            currentAim.SpawnAimZone();
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

            var hit = currentAim.GetAimInfo(ray);
            if (hit.HasValue)
            {
                currentAim.UpdateLogic(hit.Value.point);
                if (UnityEngine.Input.GetMouseButtonDown(0))
                    Abilities[_selected].Shoot(hit.Value, team);
            }
        }
    }
}
