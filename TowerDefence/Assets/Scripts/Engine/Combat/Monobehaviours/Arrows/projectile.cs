using UnityEngine;
using System.Collections;
using Rudrac.TowerDefence.Stats;

namespace Rudrac.TowerDefence.Combat
{
    public class projectile : MonoBehaviour
    {
        [Tooltip("assign attackdefinition SO here")]
        public AttackDefinition attackDef;
        
        [Space]
        public Rigidbody rigid;
        [HideInInspector] public bool TouchingGround;
        
        [HideInInspector]
        public CharacterStats stats;
        [HideInInspector]
        public GameObject _object;
        // Update is called once per frame
        public virtual void Update()
        {
            if (!TouchingGround && rigid.velocity != Vector3.zero)
            {
                // update the rotation of the projectile during trajectory motion
                transform.rotation = Quaternion.LookRotation(rigid.velocity);
            }
        }

       public virtual void OnCollisionEnter(Collision other)
       {
            if (other.gameObject.name == "Ground" &&!TouchingGround)
            {
                TouchingGround = true;
                rigid.isKinematic = true;
                Destroy(gameObject, 3);
            }

            if (other.collider.CompareTag("Enemy") && !TouchingGround)
            {
                _object = other.gameObject;
                TouchingGround = true;
                rigid.isKinematic = true;
                rigid.Sleep();

                transform.parent = other.transform;
                Attack();
               
                Destroy(gameObject, 3);
            }
        }

        public virtual void Attack()
        {
            //Create attack
            var attack = attackDef.CreateAttack(stats, _object.GetComponent<CharacterStats>());

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