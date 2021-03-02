using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    [RequireComponent(typeof(Stats.CharacterStats))]
    public class AttackedTakeDamage : MonoBehaviour, IAttackable
    {
        Stats.CharacterStats _stats;
        private void Awake()
        {
            _stats = GetComponent<Stats.CharacterStats>();
        }
        public void OnAttack(GameObject attacker, Attack attack)
        {
            _stats.DecreaseHealth(attack.Damage);

            if (_stats.GetCurrentHealth() <= 0)
            {
                //TODO: Destroy 

                //get all destructable on this object
                //call onDestruction method
                var destructables = GetComponents<IDestructable>();

                foreach (IDestructable item in destructables)
                {
                    item.OnDestruction(attacker);
                }

            }
        }
    }
}