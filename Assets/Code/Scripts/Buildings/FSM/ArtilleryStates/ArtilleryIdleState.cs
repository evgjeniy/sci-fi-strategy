using System.Linq;
using NTC.FiniteStateMachine;
using SustainTheStrain.Units;
using UnityEngine;

namespace SustainTheStrain.Buildings.FSM
{
    public partial class ArtilleryStateMachine
    {
        private class IdleState : IState<ArtilleryStateMachine>
        {
            public ArtilleryStateMachine Initializer { get; }
            public IdleState(ArtilleryStateMachine initializer) => Initializer = initializer;

            public void OnRun()
            {
                Initializer.Timer.Time -= Time.deltaTime * Initializer.CooldownEnergyMultiplier;
                Initializer.Area.Update();

                if (!CheckTransitions()) return;
                OnOverridableRun();
            }

            protected virtual bool CheckTransitions()
            {
                if (GetTarget() != null)
                {
                    Initializer.SetState<AttackState>();
                    return false;
                }

                return true;
            }

            protected Collider GetTarget() => Initializer.Area.Entities
                .FirstOrDefault(e => e.TryGetComponent<Damageble>(out var d) && d.Team != 1);

            protected virtual void OnOverridableRun() {}
        }
    }
}