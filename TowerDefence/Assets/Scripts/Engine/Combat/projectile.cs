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
        private bool TouchingGround;
        
        [HideInInspector]
        public CharacterStats stats;

        // Update is called once per frame
        void Update()
        {
            if (!TouchingGround && rigid.velocity != Vector3.zero)
            {
                // update the rotation of the projectile during trajectory motion
                transform.rotation = Quaternion.LookRotation(rigid.velocity);
            }
        }
        

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Ground" &&!TouchingGround)
            {
                TouchingGround = true;
                rigid.isKinematic = true;
                Destroy(gameObject, 3);
            }

            if (other.collider.CompareTag("Enemy") && !TouchingGround)
            {
                transform.parent = other.transform;
                //Create attack
                var attack = attackDef.CreateAttack(stats, other.collider.GetComponent<CharacterStats>());

                Debug.Log("enemy collided");

                //Get all attackables on the enemy
                var attackables = other.collider.GetComponentsInParent<IAttackable>();

                //call interfase function on each attackables
                foreach (IAttackable attackable in attackables)
                {
                    attackable.OnAttack(stats.gameObject, attack);
                }

                TouchingGround = true;
                rigid.isKinematic = true;
                Destroy(gameObject, 3);
            }
        }

    }
}