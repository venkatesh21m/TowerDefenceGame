using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rudrac.TowerDefence
{
    [CreateAssetMenu(fileName = "stats", menuName = "TowerDefence/stats/Character Stats", order = 1)]
    public class characterStats_SO : ScriptableObject
    {
        #region variables

        [Header("Movement")]
        [Tooltip("How fast the character should move")]
        //Movement
        public float _Speed;




        //Health
        [Header("Health")]
        [Tooltip("Maximum ammount of health character can have")]
        [Range(0,100)]
        public float _MaxHealth;
        
        [Tooltip("Current health of the character")]
        [Range(0, 100)]
        public float _CurrentHealth;




        //Damage and Resistance
        [Header("Damage & Resistance")]
        [Tooltip("Type of damage character causes to opponent")]
        [EnumFlagsAttribute]
        public DamageType _DamageType;
       
        [Tooltip("Type of damage this character is week towards")]
        [EnumFlagsAttribute]
        public DamageType _Weekness;
        
        [Tooltip("Type of damage this character can resist ")]
        [EnumFlagsAttribute]
        public DamageType _Resistance;
        
        [Tooltip("time between each attack")]
        [Range(0,20)]
        public float _AttackRate;
        
        [Tooltip("amount of damage this character can cause on attack")]
        public float _HitPoints;

        public Combat.Weapon _Weapon;



        //curency
        [Header("Currency")]
        [Tooltip("Amount of gold this character has")]
        public float _Gold;
        [Tooltip("Ammount of gold this character costs to use")]
        public float _Cost;


        //progress
        [Header("Progress")]
        [Tooltip("character's current experience points")]
        public float _Experience;
        [Tooltip("Character's current Rank")]
        public CharacterRank _CurrentRank;



        //all ranks info
        [Header("Ranks Data")]
        [Tooltip("Info of Ranks in array")]
        public CharacterRank[] _Ranks;

        #endregion

        #region StatIncreasers
        public void AdSpeed(float amount)
        {
            _Speed += amount;
        }

        public void AddHealth(float amount)
        {
            if (_CurrentHealth + amount > _MaxHealth)
                _CurrentHealth = _MaxHealth;
            else
                _CurrentHealth += amount;
        }

        public void AddGold(float amount)
        {
            _Gold += amount;
        }

        public void AddExperience(float amount)
        {
            _Experience += amount;
            if(_Experience >= _CurrentRank._RequiredExperience)
            {
                IncreaseRank();
            }
        }

        void IncreaseRank()
        {
            for (int i = 0; i < _Ranks.Length; i++)
            {
                if(_Ranks[i] == _CurrentRank)
                {
                    _CurrentRank = _Ranks[i + 1];
                    break;
                }
            }

            _MaxHealth = _CurrentRank._MaxHealth;
            _AttackRate = _CurrentRank._AttackRate;
            _HitPoints = _CurrentRank._Hitpoints;

        }
        #endregion

        #region Stat Decreasers
        
        public void DecreaseSpeed(float amount)
        {
            _Speed -= amount;
        }
        
        public void DecreaseHealth(float amount)
        {
            _CurrentHealth -= amount;

            if(_CurrentHealth <= 0)
            {
                //Dead
                
            }
        }

        public void SpendGold(float amount)
        {
            _Gold -= amount;
            if (_Gold <= 0)
            {
                _Gold = 0;
            }
        }

        #endregion

        #region WeaponChange

        #endregion

    }

}

