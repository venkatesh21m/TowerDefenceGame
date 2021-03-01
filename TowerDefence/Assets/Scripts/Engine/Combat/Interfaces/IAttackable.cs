using UnityEngine;

namespace Rudrac.TowerDefence.Combat 
{
    public interface IAttackable
    {
        //function to call when character is attacked
        public void OnAttack(GameObject attacker, Attack attack);
    }
}
