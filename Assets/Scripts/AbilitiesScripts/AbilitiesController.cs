using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
    private List<BaseAbility> abilities = new List<BaseAbility>();
    private int mode = -1;
    [SerializeField] GameObject aimSpherePrefab;
    private GameObject aimSphere;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layersToHit;
    [SerializeField] float zoneDamageMaxDistFromCamera;
    [SerializeField] float zoneDamageRadius;
    [SerializeField] float zoneDamageReloadingSpeed;
    public delegate void ReloadDelegate(float l, bool r);
    private ReloadDelegate ZoneDamageUpdate;
    private delegate void CurrentAbilityDelegate();
    private CurrentAbilityDelegate currentAbilityUpdateLogic, currentAbilityDestroyLogic;

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
        if (mode != -1 && abilities[mode].GetType().ToString() == type) //������ ���������
        {
            mode = -1;
            currentAbilityDestroyLogic?.Invoke();
            currentAbilityDestroyLogic = currentAbilityUpdateLogic = null;
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
        aimSphere = Instantiate(aimSpherePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        aimSphere.transform.localScale = new Vector3(zoneDamageRadius, zoneDamageRadius, zoneDamageRadius);
    }

    private void ZoneDamageAbilityUpdateLogic()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, zoneDamageMaxDistFromCamera, layersToHit))
        {
            Vector3 point = hit.point;
            aimSphere.transform.position = point; //now aim is like sphere, cause my shader does not work
            if (abilities[mode].isReloaded())
            {
                //�� ���� ������� ����� �������
                if (Input.GetMouseButtonDown(0))
                    abilities[mode].Shoot(point);
            }
            else
            {
                //�� ���� ������� ����� �������
            }
        }
    }

    private void ZoneDamageAbilityDestroyLogic()
    {
        Destroy(aimSphere);
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
        currentAbilityUpdateLogic?.Invoke();
    }
}
