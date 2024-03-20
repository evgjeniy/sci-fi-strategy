using System.Collections.Generic;
using SustainTheStrain.Scriptable.AbilitySettings;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Abilities
{
    public class ChainDamageAbility : PointAbility
    {
        protected float damage;
        protected int maxTargetsCount;
        protected float maxDistanceBetween;
        protected GameObject LinePref;
        protected GameObject LineObject;

        public ChainDamageAbility(ChainDamageAbilitySettings settings)
        {
            LinePref = settings.LinePrefab;
            LoadingSpeed = settings.ReloadingSpeed;
            damage = settings.Damage;
            maxTargetsCount = settings.MaxTargets;
            maxDistanceBetween = settings.Distance;
            SetEnergySettings(settings.EnergySettings);
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
            var stdmg = hit.collider.GetComponent<Damageble>();
            if (stdmg == null || stdmg.Team == team)
            {
                FailShootLogic();
                return;
            }
            Reload = 0;
            SuccessShootLogic(hit, team);
        }

        private LineRenderer setLineRenderer()
        {
            LineObject = GameObject.Instantiate(LinePref);
            LineObject.SetActive(false);
            var line = LineObject.GetComponentInChildren<LineRenderer>();
            line.positionCount = 0;
            line.startWidth = line.endWidth = 0.2f;
            LineObject.SetActive(true);
            return line;
        }

        protected override void SuccessShootLogic(RaycastHit hit, int team)
        {
            var line = setLineRenderer();

            var curTarget = hit.collider;
            Collider[] curColliders;
            List<Collider> used = new(maxTargetsCount);

            used.Add(hit.collider);
            curTarget.GetComponent<Damageble>().Damage(damage);

            line.positionCount++;
            line.SetPosition(line.positionCount - 1, curTarget.transform.position);

            //Debug.Log(curTarget.GetComponent<Units.Components.Damageble>().Team);
            while (line.positionCount < maxTargetsCount)
            {
                curColliders = Physics.OverlapSphere(curTarget.transform.position, maxDistanceBetween);
                int nearestIdx = -1;
                float bestDistance = float.MaxValue;

                for (int i = 0;i < curColliders.Length; i++)
                {
                    var temp = curColliders[i];
                    var dmg = temp?.GetComponent<Damageble>();

                    if (dmg == null || dmg.Team == team || used.Contains(temp)) continue;

                    float tempDst = Vector3.Distance(temp.transform.position, curTarget.transform.position);
                    if (tempDst < bestDistance)
                    {
                        nearestIdx = i;
                        bestDistance = tempDst;
                    }
                }

                if (nearestIdx == -1) break;

                curTarget = curColliders[nearestIdx];
                curTarget.GetComponent<Damageble>().Damage(damage);
                used.Add(curTarget);
                //Debug.Log(dmg.Team);

                line.positionCount++;
                line.SetPosition(line.positionCount - 1, curTarget.transform.position);
            }

            LineObject.AddComponent<ChainUpdate>();
            var upd = LineObject.GetComponent<ChainUpdate>();
            upd.setFields(2, used); //hardcode
        }

        protected override void ReadyToShoot()
        {
            Debug.Log("CHAIN ready to shoot");
        }
    }
}