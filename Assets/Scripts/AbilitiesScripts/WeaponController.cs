using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private List<BaseAbility> abilities = new List<BaseAbility>();
    private int mode = 0;
    private ZoneDamageAbility zoneDmg;
    [SerializeField] GameObject aimSpherePrefab;
    private GameObject aimSphere;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask layersToHit;
    [SerializeField] float zoneDamageMaxDistFromCamera;
    [SerializeField] float zoneDamageRadius;
    [SerializeField] float zoneDamageReloadingSpeed;
    private UIController uiController;

    private void Start()
    {
        uiController = GetComponent<UIController>();
        zoneDmg = new ZoneDamageAbility(zoneDamageRadius, aimSpherePrefab, zoneDamageReloadingSpeed);
        addAbility(zoneDmg);
    }

    public void addAbility(BaseAbility ability)
    {
        abilities.Add(ability);
    }

    public void resetAbilities()
    {
        abilities.Clear();
    }

    public void OnZoneDamageSelect()
    {
        if (mode == 1)
        {
            mode = 0;
            Destroy(aimSphere);
            return;
        }
        aimSphere = Instantiate(aimSpherePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        aimSphere.transform.localScale = new Vector3(zoneDamageRadius, zoneDamageRadius, zoneDamageRadius);
        mode = 1;
    }

    void Update()
    {
        foreach (var ability in abilities)
            if (!ability.canShoot())
            {
                ability.Load(Time.deltaTime);
                if (ability is ZoneDamageAbility)   //tut poka tak budet, Ilya ne bei, mozhet potom chto to peredelaem
                    uiController.setZoneDamageButtonData(ability.getReload(), ability.canShoot());
            }
        switch (mode)
        {
            case 1:
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = mainCamera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, zoneDamageMaxDistFromCamera, layersToHit))
                {
                    Vector3 point = hit.point;
                    aimSphere.transform.position = point; //now aim is like sphere, cause my shader does not work
                    if (zoneDmg.canShoot())
                    {
                        //aimRenderer.material.SetColor("_Color", new Color(10, 10, 10));
                        if (Input.GetMouseButtonDown(0))
                            zoneDmg.Shoot();
                    }
                    else
                    {
                        //aimRenderer.material.SetColor("_Color", new Color(0, 100, 50));
                    }
                }
                break;
        }
    }
}
