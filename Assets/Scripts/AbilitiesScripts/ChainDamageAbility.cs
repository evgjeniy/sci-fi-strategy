using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain.AbilitiesScripts
{
    public class ChainDamageAbility : PointAbility
    {
        protected float damage;
        protected int maxTargetsCount;
        protected float maxDistanceBetween;
        protected GameObject LinePrefab;

        public ChainDamageAbility(GameObject pref, float speed, float dmg, int maxtrg, float dst)
        {
            LinePrefab = GameObject.Instantiate(pref);
            LinePrefab.SetActive(false);
            LoadingSpeed = speed;
            damage = dmg;
            maxTargetsCount = maxtrg;
            maxDistanceBetween = dst;
        }

        protected override void FailShootLogic()
        {
            Debug.Log("CHAIN failed to shoot");
        }

        public override void Shoot(RaycastHit hit, int team)
        {
            if (!IsReloaded())
            {
                FailShootLogic();
                return;
            }
            var stdmg = hit.collider.GetComponent<Units.Components.Damageble>();
            if (stdmg == null || stdmg.Team == team)
            {
                FailShootLogic();
                return;
            }
            Reload = 0;
            SuccessShootLogic(hit, team);
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            var line = LinePrefab.GetComponentInChildren<LineRenderer>();
            line.positionCount = 0;
            line.startWidth = 2.5f;
            line.endWidth = 2.5f;
            LinePrefab.SetActive(true);
            var curTarget = hit.collider;
            Collider[] curColliders;
            List<Collider> used = new(maxTargetsCount);
            used.Add(hit.collider);
            curTarget.GetComponent<Units.Components.Damageble>().Damage(damage);
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, curTarget.transform.position);
            Debug.Log(curTarget.GetComponent<Units.Components.Damageble>().Team);
            bool fl = true;
            while (fl && line.positionCount < maxTargetsCount)
            {
                curColliders = Physics.OverlapSphere(curTarget.transform.position, maxDistanceBetween);
                fl = false;
                for (int i = 0; !fl && i < curColliders.Length; i++)
                {
                    curTarget = curColliders[i];
                    var dmg = curTarget.GetComponent<Units.Components.Damageble>();
                    if (dmg == null || dmg.Team == team || used.Contains(curTarget)) continue;
                    dmg.Damage(damage);
                    used.Add(curTarget);
                    Debug.Log(dmg.Team);
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, curTarget.transform.position);
                    fl = true;
                }
            }
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("CHAIN ready to shoot");
        }
    }
}
