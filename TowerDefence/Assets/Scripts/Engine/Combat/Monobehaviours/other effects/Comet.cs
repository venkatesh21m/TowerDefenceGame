using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class Comet : MonoBehaviour
    {
        [Tooltip("assign attackdefinition SO here")]
        [HideInInspector]
        public AttackDefinition attackDef;
        [HideInInspector]
        public Stats.CharacterStats stats;
        public Rigidbody rigid;
        private GameObject _object;
        public GameObject ICeBlast;

        public bool TouchingGround { get; private set; }

        public void OnCollisionEnter(Collision other)
        {

            if (!TouchingGround)
            {
                Destroy(Instantiate(ICeBlast, other.GetContact(0).point, ICeBlast.transform.rotation), 1);
            }

            if (other.gameObject.name == "Ground" && !TouchingGround)
            {
                TouchingGround = true;
                rigid.isKinematic = true;
                Destroy(gameObject, 1.5f);
            }

            if (other.collider.CompareTag("Enemy") && !TouchingGround)
            {
                _object = other.gameObject;
                TouchingGround = true;
                rigid.isKinematic = true;
                rigid.Sleep();

                transform.parent = other.transform;
                Attack();

                Destroy(gameObject, 1.5f);
            }
            
        }

        public virtual void Attack()
        {
            //Create attack
            var attack = attackDef.CreateAttack(stats, _object.GetComponent<Stats.CharacterStats>());

            Debug.Log("enemy collided");

            //Get all attackables on the enemy
            var attackables = _object.GetComponentsInParent<IAttackable>();

            //call interfase function on each attackables
            foreach (IAttackable attackable in attackables)
            {
                attackable.OnAttack(stats.gameObject, attack);
            }

        }

    }
}
