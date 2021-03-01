using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rudrac.TowerDefence.Stats;

namespace Rudrac.TowerDefence.Combat
{
    [CreateAssetMenu(fileName = "Attack.asset",menuName = "TowerDefence/Combat/Attack", order =1)]
    public class AttackDefinition : ScriptableObject
    {
        #region Variables
        public float Cooldown;
        public float Range;
        public float MinDamage;
        public float MaxDamage;
        public float CriticalMultiplier;
        public float CriticalChance;
        #endregion
       
        
        public Attack CreateAttack(CharacterStats weilderStats, CharacterStats DefenderStats)
        {
            //Getting hit points from weilder
            float coreDamage = weilderStats.GetHitPoints();
            
            //multiplying hitpoints with weapon damage of random btw min and max 
            coreDamage += Random.Range(MinDamage, MaxDamage);
            
            //Random critical check
            bool isCritical = Random.value < CriticalChance;
            //if critical multiply with critical value
            if (isCritical)
                coreDamage *= CriticalMultiplier;

            //if defender stats not null
            if (DefenderStats != null)
            {
                //compare defernder stasts resistance to weilderdamage 
                // if similer decrease ammount of damage

                var deferenderResistances = DefenderStats.GetResistance();
                foreach (var item in deferenderResistances)
                {
                    if (weilderStats.GetDamageType().ToString() ==  item)
                    {
                        coreDamage /= 2.5f;
                    }
                }
                
            }

            //Create attack with damage values and return 
            return new Attack(coreDamage, isCritical);
        }
    }
}
