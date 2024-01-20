using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AbilitiesController : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0.8f, 0);
    private List<BaseAbility> abilities = new List<BaseAbility>();
    private int mode = -1;
    [SerializeField] GameObject aimZonePrefab;
    private GameObject aimZone;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layersToHit;
    [SerializeField] float zoneDamageMaxDistFromCamera;
    [SerializeField] float zoneDamageRadius;
    [SerializeField] float zoneDamageReloadingSpeed;
    public delegate void ReloadDelegate(float l, bool r);
    private ReloadDelegate ZoneDamageUpdate;
    private delegate void CurrentDestroyDelegate();
    private CurrentDestroyDelegate currentAbilityDestroyLogic;
    private delegate void CurrentUpdateDelegate(RaycastHit hit);
    CurrentUpdateDelegate currentAbilityUpdateLogic;

    private void Start()
    {
        AddAbility(new ZoneDamageAbility(zoneDamageRadius, zoneDamageReloadingSpeed));
    }

    public void ZoneDamageAdd(ReloadDelegate rd)
    {
        ZoneDamageUpdate += rd;
    }

    public void AddAbility(BaseAbility ability)
    {
        abilities.Add(ability);
    }

    public void ResetAbilities()
    {
        abilities.Clear();
    }

    private int FindAbitility(string type)
    {
        int cnt = 0;
        while (cnt < abilities.Count && abilities[cnt].GetType().ToString() != type)
            cnt++;
        return (cnt == abilities.Count ? -1 : cnt);
    }

    private bool isCurrentSelected(string type)
    {
        if (mode != -1 && abilities[mode].GetType().ToString() == type) //нажали выбранную
        {
            mode = -1;
            currentAbilityDestroyLogic?.Invoke();
            currentAbilityDestroyLogic = null;
            currentAbilityUpdateLogic = null;
            return true;
        }
        return false;
    }

    public void OnZoneDamageAbilitySelect()
    {
        string typeName = "ZoneDamageAbility";
        if (isCurrentSelected(typeName))
            return;
        mode = FindAbitility(typeName);
        if (mode == -1)
            return;
        currentAbilityUpdateLogic += ZoneDamageAbilityUpdateLogic;
        currentAbilityDestroyLogic += ZoneDamageAbilityDestroyLogic;
        aimZone = Instantiate(aimZonePrefab, new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));
        aimZone.GetComponent<DecalProjector>().size = new Vector3(zoneDamageRadius, zoneDamageRadius, 25);
    }

    private void ZoneDamageAbilityUpdateLogic(RaycastHit hit)
    {
        Vector3 point = hit.point;
        aimZone.transform.position = point + offset;
        if (abilities[mode].isReloaded())
        {
            //мб цвет прицела будет зеленый
            if (Input.GetMouseButtonDown(0))
                abilities[mode].Shoot(point);
        }
        else
        {
            //мб цвет прицела будет красный
        }
    }

    private void ZoneDamageAbilityDestroyLogic()
    {
        Destroy(aimZone);
    }

    void Update()
    {
        foreach (var ability in abilities)
            if (!ability.isReloaded())
            {
                ability.Load(Time.deltaTime);
                if (ability is ZoneDamageAbility)
                    ZoneDamageUpdate.Invoke(ability.getReload(), ability.isReloaded());
            }
        if (mode == -1)
            return;
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, zoneDamageMaxDistFromCamera, layersToHit))
        {
            currentAbilityUpdateLogic?.Invoke(hit);
        }
    }
}
