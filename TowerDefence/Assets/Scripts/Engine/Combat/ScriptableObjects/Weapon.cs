using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rudrac.TowerDefence;

namespace Rudrac.TowerDefence.Combat
{
    [CreateAssetMenu(fileName ="Wapon", menuName = "TowerDefence/Combat/Weapon", order = 2)]
    public class Weapon : AttackDefinition
    {
        //reference to the weapon arrow
        //if player selects this prefab will be fired
        public Rigidbody weapomPrefab;
        
        //Damage types of this weapon
        [EnumFlagsAttribute]
        public DamageType damagetype;
        [Space]
        public Stats.WeaponStats weaponststs;

        //function to perform attack
        public void ExecuteAttack(GameObject attacker, GameObject defender)
        {
            if (defender == null) return;

            //caching stats
            var attackerStats = attacker.GetComponent<Stats.CharacterStats>();
            var defenderStats = defender.GetComponent<Stats.CharacterStats>();

            //create attack
            var attack = CreateAttack(attackerStats, defenderStats);
            
            //collect all attackable interfaces in defender object
            var attackables = defender.GetComponents<IAttackable>();

            //call OnAttack on every attackable
            foreach (IAttackable attackable in attackables)
            {
                attackable.OnAttack(attacker, attack);
            }
        }
    }
}