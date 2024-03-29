using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence
{
    [System.Serializable]
    public class Rank 
    {
        public string RankName;

        [Tooltip("refrence to rank number")]
        public int _Ranknumber;

        //experience value to reach this rank
        [Header("Requirements")]
        [Tooltip("ammount of experience character need to go to next Rank")]
        public float _RequiredExperience;

        [Tooltip("ammount of AttackRate Upgrade for reaching this rank")]
        public float _AttackRate;
        [Tooltip("ammount of HitPoint Upgrade for reaching this rank")]
        public float _Hitpoints;
    }
}
