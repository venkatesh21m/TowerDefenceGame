using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Rudrac.TowerDefence
{
    [System.Serializable]
    public class CharacterRank : Rank
    {

        //rewards for reaching the rank
        [Header("Rewards")]
        [Tooltip("ammount of MaxHealth Upgrade for reaching this rank")]
        public float _MaxHealth;
       

    }
}