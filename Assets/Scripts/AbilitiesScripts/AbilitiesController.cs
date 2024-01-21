using System;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
    public delegate void ReloadDelegate(int idx, float l, bool r);
    private Vector3 offset = new Vector3(0, 0.8f, 0), nullVector = new Vector3(0, 0, 0);
    public List<BaseAbility> abilities = new List<BaseAbility>(); //better to make it readonly
    private ReloadDelegate[] reloadList; //tut private, dlya dobavlenia est metod
    private int selected = -1;
    [SerializeField] GameObject aimZonePrefab;
    private GameObject aimZone;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layersToHit;
    [SerializeField] float zoneDamageMaxDistFromCamera;
    [SerializeField] float zoneDamageRadius;
    [SerializeField] float zoneDamageReloadingSpeed;

    public void Init() //temporary, because now we don't have MainController
    {
        AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed));
        AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed));
        AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed));
        reloadListSyncSize(); //когда все абилки добавлены
    }

    private void Start()
    {
        
    }

    public void reloadListSyncSize()
    {
        reloadList = new ReloadDelegate[abilities.Count];
    }

    public void reloadListAdd(int idx, ReloadDelegate rd)
    {
        if (idx < 0 || idx >= reloadList.Length)
            throw new IndexOutOfRangeException("Incorrect idx");
        reloadList[idx] = rd;
    }

    public void AddAbility(BaseAbility ability)
    {
        abilities.Add(ability);
    }

    public void ResetAbilities()
    {
        reloadList = null;
        abilities.Clear();
    }

    private bool isCurrentSelected(int type)
    {
        if (selected == type) //нажали выбранную
        {
            abilities[selected].DestroyLogic();
            selected = -1;
            return true;
        }
        return false;
    }

    public void OnAbilitySelect(int idx)
    {
        if (isCurrentSelected(idx))
            return;
        selected = idx;
        if (abilities[selected] is ZoneAbility)
            (abilities[selected] as ZoneAbility).setAimZone(Instantiate(aimZonePrefab, nullVector, Quaternion.Euler(90, 0, 0)));
    }

    void Update()
    {
        for(int i = 0;i < abilities.Count;i++)
            if (!abilities[i].isReloaded())
            {
                abilities[i].Load(Time.deltaTime);
                reloadList[i].Invoke(i, abilities[i].getReload(), abilities[i].isReloaded());
            }
        if (selected == -1)
            return;
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, zoneDamageMaxDistFromCamera, layersToHit))
            abilities[selected].UpdateLogic(hit);
    }
}
