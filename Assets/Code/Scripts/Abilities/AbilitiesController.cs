using System;
using System.Collections.Generic;
using SustainTheStrain.EnergySystem;
using SustainTheStrain.Input;
using SustainTheStrain.Scriptable.AbilitySettings;
using SustainTheStrain.Units;
using UnityEngine;
using Zenject;

namespace SustainTheStrain.Abilities
{
    public class AbilitiesController : MonoBehaviour
    {
        [SerializeField] private AbilitiesListSettings _abilitiesSettings;
        [SerializeField] private GameObject aimZonePrefab;
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private LayerMask enemyLayers;
        [SerializeField] private int maxDistFromCamera;
        [SerializeField] private float recruitAimRadius;
        [SerializeField] private Team team = Team.Player;
        [Inject] private EnergyController _energyController;

        public static bool freezeSelected = false;
        public static List<GameObject> activeSquads = new();
        public static int maxSquads = 2;

        private BaseAim currentAim;

        private IAbilityInput _abilityInput;

        public readonly List<BaseAbility> Abilities = new(); //better to make it readonly

        private ReloadDelegate[] _reloadList; //tut private, dlya dobavlenia est metod
        private int _selected = -1;

        public delegate void ReloadDelegate(int idx, float l, bool r);

        [Inject]
        private void Construct(IAbilityInput inp)
        {
            _abilityInput = inp;
        }

        private void OnEnable()
        {
            _abilityInput.OnAbilityChanged += OnAbilityChanged;
            _abilityInput.OnAbilityMove += MoveMethod;
            _abilityInput.OnAbilityClick += UseAbility;
            _abilityInput.OnAbilityEnter += EnterAbility;
            _abilityInput.OnAbilityExit += ExitAbilityl;
        }

        private void OnAbilityChanged(int obj)
        {
            ExitAbilityl(_selected + 1);
            EnterAbility(obj);
        }

        private void ExitAbilityl(int idx)
        {
            idx--;
            if (idx != _selected) return;
            _selected = -1;
            currentAim?.Destroy();
            currentAim = null;
            freezeSelected = false;
        }

        private void EnterAbility(int idx)
        {
            idx--;
            var chosenAbility = Abilities[idx];
            if (!chosenAbility.IsLoaded)
            {
                return;
            }
            if (!chosenAbility.IsReloaded()) return;
            currentAim = Abilities[idx] switch
            {
                FreezeAbility => null,
                ZoneAbility => new ZoneAim((Abilities[idx] as ZoneAbility).getZoneRadius(), aimZonePrefab, groundLayers, maxDistFromCamera),
                LandingAbility => new ZoneAim(recruitAimRadius, aimZonePrefab, groundLayers, maxDistFromCamera),
                _ => new PointAim(enemyLayers, maxDistFromCamera)
            };

            if (currentAim != null)
                currentAim.SpawnAimZone();
            _selected = idx;

            if (Abilities[_selected] is FreezeAbility)
                freezeSelected = true;
        }

        private void UseAbility(Ray ray)
        {
            if (_selected == -1) return;

            if (Abilities[_selected] is FreezeAbility)
            {
                Abilities[_selected].Shoot(new RaycastHit(), team);
                return;
            }

            var hit = currentAim.GetAimInfo(ray);
            if (hit.HasValue)
                Abilities[_selected].Shoot(hit.Value, team);
        }

        private void MoveMethod(Ray ray)
        {
            if (currentAim == null) return;
            var hit = currentAim.GetAimInfo(ray);
            if (hit.HasValue)
                currentAim.UpdateLogic(hit.Value);
        }

        private void OnDisable()
        {
            ExitAbilityl(_selected + 1);
            _abilityInput.OnAbilityChanged -= OnAbilityChanged;
            _abilityInput.OnAbilityMove -= MoveMethod;
            _abilityInput.OnAbilityClick -= UseAbility; 
            _abilityInput.OnAbilityEnter -= EnterAbility;
            _abilityInput.OnAbilityExit -= ExitAbilityl;
        }

        public void Init() //temporary, because now we don't have MainController
        {
            AddAbility(new ZoneDamageAbility(_abilitiesSettings.ZoneDamage));
            //AddAbility(new ZoneSlownessAbility(_abilitiesSettings.ZoneSlowness));
            AddAbility(new FreezeAbility(_abilitiesSettings.ZoneSlowness));
            AddAbility(new ChainDamageAbility(_abilitiesSettings.ChainAbility));
            //AddAbility(new EnemyHackAbility(_abilitiesSettings.EnemyHack));
            AddAbility(new LandingAbility(_abilitiesSettings.LandingAbility));
            foreach (var ability in Abilities)
            {

                _energyController.AddEnergySystem(ability);
            }
            ReloadListSyncSize(); //êîãäà âñå àáèëêè äîáàâëåíû
        }

        public void setTeamOpponent(Team team) => this.team = team;

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

        private void Update()
        {
            if (_selected != -1 && Abilities[_selected] is ChainDamageAbility or EnemyHackAbility)
            {
                Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, maxDistFromCamera, enemyLayers))
                    hit.collider.GetComponent<OutLineControll>()?.setMark();
            }
            for (var i = 0; i < Abilities.Count; i++)
            {
                if (Abilities[i].IsReloaded()) continue;
                
                Abilities[i].Load(Time.deltaTime);
                _reloadList[i].Invoke(i, Abilities[i].GetReload(), Abilities[i].IsReloaded());
            }
        }
    }

    [Serializable]
    public class AbilitiesListSettings
    {
        public ChainDamageAbilitySettings ChainAbility;
        public EnemyHackAbilitySettings EnemyHack;
        public LandingAbilitySettings LandingAbility;
        public ZoneDamageAbilitySettings ZoneDamage;
        public ZoneSlownessAbilitySettings ZoneSlowness;
    }
}