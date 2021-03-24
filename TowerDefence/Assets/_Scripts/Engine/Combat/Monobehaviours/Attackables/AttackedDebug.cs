using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class AttackedDebug : MonoBehaviour, IAttackable
    {

        /// <summary>
        /// this class is just to check the combat system works or not
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="attack"></param>
        public void OnAttack(GameObject attacker, Attack attack)
        {
            Debug.LogFormat("{0} attacked {1} for {2} damage", attacker.name, name, attack.Damage);
            if (attack.isCritical)
            {
                Debug.Log("Critacal attack");
            }
        }
    }
}
