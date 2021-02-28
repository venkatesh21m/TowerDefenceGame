using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.CharacterStats
{
    public class CharacterStats : MonoBehaviour
    {
        [SerializeField] characterStats_SO CharacterStatsDefinition;

        // Start is called before the first frame update
        void Start()
        {
           
        }

        #region StatIncreasers
        public void AdSpeed(float amount)
        {
            CharacterStatsDefinition.AdSpeed(amount);
        }

        public void AddHealth(float amount)
        {
            CharacterStatsDefinition.AddHealth(amount);
        }

        public void AddGold(float amount)
        {
            CharacterStatsDefinition.AddGold(amount);
        }

        public void AddExperience(float amount)
        {
            CharacterStatsDefinition.AddExperience(amount);
        }
       
        #endregion

        #region Stat Decreasers

        public void DecreaseSpeed(float amount)
        {
            CharacterStatsDefinition.DecreaseSpeed(amount);
        }

        public void DecreaseHealth(float amount)
        {
            CharacterStatsDefinition.DecreaseHealth(amount);
        }

        public void SpendGold(float amount)
        {
            CharacterStatsDefinition.SpendGold(amount);
        }

        #endregion

        #region Accessors

        public float GetCurrentHealth()
        {
            return CharacterStatsDefinition._CurrentHealth;
        }

        public float GetCurremtSpeed()
        {
            return CharacterStatsDefinition._Speed;
        }
        public DamageType GetDamageType()
        {
            return CharacterStatsDefinition._DamageType;
        }
        public DamageType GetResistance()
        {
            return CharacterStatsDefinition._Resistance;
        }

        public DamageType GetWeakness()
        {
            return CharacterStatsDefinition._Weekness;
        }

        public float GetExperice()
        {
            return CharacterStatsDefinition._Experience;
        }

        public float GetCurrentRank()
        {
            return CharacterStatsDefinition._CurrentRank._Ranknumber;
        }

        public float GetCost()
        {
            return CharacterStatsDefinition._Cost;
        }

        public float GetTotalGold()
        {
            return CharacterStatsDefinition._Gold;
        }

        public float GetHitPoints()
        {
            return CharacterStatsDefinition._HitPoints;
        }

        public float GetAttackRate()
        {
            return CharacterStatsDefinition._AttackRate;
        }

        #endregion
    }
}
