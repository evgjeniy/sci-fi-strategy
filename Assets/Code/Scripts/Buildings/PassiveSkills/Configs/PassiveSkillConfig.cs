using UnityEngine;

namespace SustainTheStrain.Buildings
{
    public abstract class PassiveSkillConfig : ScriptableObject
    {
        public abstract void EnableSkill(GameObject gameObject);
        public abstract void DisableSkill(GameObject gameObject);
    }
}