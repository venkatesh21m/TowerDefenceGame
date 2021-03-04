using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Rudrac.TowerDefence.Combat;
using Rudrac.TowerDefence.Stats;

namespace Rudrac.TowerDefence.AI
{
    public class AIMovement : MonoBehaviour
    {
        Stats.CharacterStats stats;

        public Animator anim;
        public NavMeshAgent agent;

        bool attacking = false;
        bool canAttack = true;
        public Transform Target;

        private void Start()
        {
            stats = FindObjectOfType<Stats.CharacterStats>();

            agent.speed = stats.GetSpeed();
            agent.SetDestination(Target.position);
        }

        void Update()
        {
            if(agent != null)
                anim.SetFloat("MovementSpeed", agent.velocity.magnitude);

            if (Target == null)
            {
                Target = FindObjectOfType<Tower>().transform;
                agent.SetDestination(Target.position);
            }

            if (Vector3.Distance(Target.position, transform.position) < stats.GetWeapon().Range)
            {
                if (canAttack)
                {
                    canAttack = false;
                    Attack();
                    anim.SetTrigger("Attack");
                    Invoke("ResetAttack", stats.GetAttackRate());
                }
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerSideTag>())
            {
                Target = other.transform;
                agent.SetDestination(Target.position);
            }
        }

        void ResetAttack()
        {
            canAttack = true;
        }

        public void Attack()
        {
            var attack = stats.GetWeapon().CreateAttack(stats, Target.GetComponent<CharacterStats>());

            Debug.Log("enemy collided");

            //Get all attackables on the enemy
            var attackables = Target.GetComponentsInParent<IAttackable>();

            //call interfase function on each attackables
            foreach (IAttackable attackable in attackables)
            {
                attackable.OnAttack(stats.gameObject, attack);
            }
        }
    }

    public enum Direction
    {
        right,
        left
    }

}
