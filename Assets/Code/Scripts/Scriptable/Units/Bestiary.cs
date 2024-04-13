using SustainTheStrain.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SustainTheStrain
{
    [CreateAssetMenu(fileName = "Bestiary", menuName = "Catalogues/Bestiary")]
    public class Bestiary : ScriptableObject
    {
        [SerializeField] private List<EnemyItem> _enemiesCatalogue;

        public List<EnemyItem> Catalogue => _enemiesCatalogue;

        [Serializable]
        public struct EnemyItem 
        { 
            [SerializeField] public string name;
            [SerializeField] public int id;
            [SerializeField] public Enemy enemy;
        }
    }
}
