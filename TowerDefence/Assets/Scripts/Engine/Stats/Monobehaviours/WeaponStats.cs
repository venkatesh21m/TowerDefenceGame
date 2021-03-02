using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Stats
{
    [CreateAssetMenu(fileName ="WeaponStats",menuName = "TowerDefence/stats/Weapon stats")]
    public class WeaponStats : ScriptableObject
    {
        public float HitPoint;
        public float Experiecne;
        public Rank _CurrentRank;

        [Header("Ranks info")]
        public Rank[] Ranks;

    }
}
