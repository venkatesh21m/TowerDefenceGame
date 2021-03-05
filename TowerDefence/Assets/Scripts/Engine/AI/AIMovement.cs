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
            if (stats.enemy)
            {
                Target = Managers.LevelManager.instance.playerflag;
                gameObject.AddComponent<EnemySideTag>();
            }
            if (stats.playerTroop)
            {
                Target = Managers.LevelManager.instance.Enemyflag;
                gameObject.AddComponent<PlayerSideTag>();
            }
            agent.speed = stats.GetSpeed();
            agent.SetDestination(Target.position);
        }

        void Update()
        {
            if(agent != null)
                anim.SetFloat("MovementSpeed", agent.velocity.magnitude);

            if (Target == null)
            {
                if (stats.enemy)
                {
                    Target = Managers.LevelManager.instance.playerflag;
                }
                if (stats.playerTroop)
                {
                    Target = Managers.LevelManager.instance.Enemyflag;
                }
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

            //store all collided enemies 
            // set destination to one
            //after killing it change destinaton
            //after killing all the enemies
            //assign the flag pos


            if (Target.GetComponent<CharacterStats>()) return;
            if (agent == null) return;
            if (stats.enemy)
            {
                if (other.GetComponentInParent<PlayerSideTag>())
                {
                    Target = other.GetComponentInParent<CharacterStats>().transform;
                    agent.SetDestination(Target.position);
                    Debug.Log("detected playertroop");
                }
            }
            else if(stats.playerTroop)
            {
                if (other.GetComponentInParent<EnemySideTag>())
                {
                    Target = other.GetComponentInParent<CharacterStats>().transform;
                    Debug.Log("detected enemy");

                    agent.SetDestination(Target.position);
                }
            }
        }

        void ResetAttack()
        {
            canAttack = true;
        }

        public void Attack()
        {
            if (Target == null) return;

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
