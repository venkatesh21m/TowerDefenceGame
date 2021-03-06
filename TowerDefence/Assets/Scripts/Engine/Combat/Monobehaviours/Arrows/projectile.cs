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

            if (stats.enemy || stats.playerTroop)
            {
                if (stats.enemy && other.transform.CompareTag("Player"))
                {
                    if (other.transform.GetComponentInParent<Stats.CharacterStats>().GetComponent<PlayerSideTag>())
                    {
                        _object = other.transform.GetComponentInParent<Stats.CharacterStats>().gameObject;

                        TouchingGround = true;
                        rigid.isKinematic = true;
                        rigid.Sleep();

                        transform.parent = other.transform;
                        Attack();
                    }
                    Destroy(gameObject, 3);

                }
                else if(stats.playerTroop && other.transform.CompareTag("Enemy"))
                {
                    if (other.transform.GetComponentInParent<Stats.CharacterStats>().GetComponent<EnemySideTag>())
                    {
                        _object = other.transform.GetComponentInParent<Stats.CharacterStats>().gameObject;

                        TouchingGround = true;
                        rigid.isKinematic = true;
                        rigid.Sleep();

                        transform.parent = other.transform;
                        Attack();
                    }

                    Destroy(gameObject, 3);
                }
            }
            else
            {
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
        }

        public virtual void Attack()
        {
            //Create attack
            var attack = attackDef.CreateAttack(stats, _object.GetComponent<CharacterStats>());

            //Get all attackables on the enemy
            var attackables = _object.GetComponentsInParent<IAttackable>();

            //call interfase function on each attackables
            foreach (IAttackable attackable in attackables)
            {
                if(stats!= null)
                    attackable.OnAttack(stats.gameObject, attack);
            }

        }
    }
}