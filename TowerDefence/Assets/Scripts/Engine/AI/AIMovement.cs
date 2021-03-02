using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Rudrac.TowerDefence.AI
{

    
    public class AIMovement : MonoBehaviour
    {
        public Direction direction;
        Stats.CharacterStats stats;

        public Animator anim;
        public NavMeshAgent agent;

        bool attacking = false;
        bool canAttack = true;
        public Transform Target;
        
        private void Start()
        {
            agent.SetDestination(Target.position);
        }

        void Update()
        {
            if (Vector3.Distance(Target.position, transform.position) < stats.GetWeapon().Range)
            {
                if (canAttack)
                {
                    canAttack = false;
                    Invoke("ResetAttack", stats.GetAttackRate());
                }
            }

            if(Target == null)
            {
                Target = FindObjectOfType<Tower>().transform;
                agent.SetDestination(Target.position);
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

    }

    public enum Direction
    {
        right,
        left
    }

}