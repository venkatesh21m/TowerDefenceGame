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
        public bool carryingflag = false;
        bool flaginpos = true;
        List<GameObject> enemies;
        Managers.LevelManager levelmanager;
        public void LoseFlag()
        {
            carryingflag = false;
            if (agent != null)
                agent.speed = stats.GetSpeed();
        }

        private void Start()
        {
            enemies = new List<GameObject>();

            stats = GetComponent<Stats.CharacterStats>();
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

            levelmanager = Managers.LevelManager.instance;

            agent.speed = stats.GetSpeed();
            agent.SetDestination(Target.position);
            InvokeRepeating("SetAgentDestination", Random.Range(0, 10), Random.Range(0, 15));


        }

        void SetAgentDestination()
        {
            if (stats.enemy)
            {
                if (!levelmanager.enemyflagnotinplace)
                {
                    flaginpos = false;
                    Target = levelmanager.Enemyflag;
                    if (agent != null)
                        agent.SetDestination(Target.position);
                }
                else
                {
                    flaginpos = true;
                    Target = levelmanager.playerflag;
                    if(agent!=null)
                        agent.SetDestination(Target.position);
                }

            } 
            
            if (stats.playerTroop)
            {
                if (!levelmanager.playerflagnotinplace)
                {
                    flaginpos = false;
                    Target = levelmanager.playerflag;
                    if (agent != null)
                        agent.SetDestination(Target.position);
                }
                else
                {
                    flaginpos = true;
                    Target = levelmanager.Enemyflag;
                    if (agent != null)
                        agent.SetDestination(Target.position);
                }

            }
        }

        void Update()
        {
            if(agent != null && anim)
                anim.SetFloat("MovementSpeed", agent.velocity.magnitude);
            Vector3 pos = transform.position;
            if (pos.x != -0.8f) transform.position = new Vector3(pos.x, pos.y, - 0.8f);

            if (Target == null)
            {
                foreach (GameObject item in enemies)
                {
                    if (item != null)
                    {
                        Target = item.transform;
                        break;
                    }
                }

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
                }
                if (agent != null )
                    agent.SetDestination(Target.position);

            }

            //to carry flag
            if (stats.playerTroop && Target.CompareTag("EnemyFlag"))
            {
                if (Vector3.Distance(Target.position, transform.position) < 2)
                {
                    if (Target.parent)
                    {
                        if (Target.parent.GetComponent<Stats.CharacterStats>().GetSpeed() > stats.GetSpeed())
                        {
                            Target.parent.GetComponent<AI.AIMovement>().LoseFlag();
                            
                            Target.parent = transform;
                            //if (!flaginpos)
                            //{
                            //    Target = Managers.LevelManager.instance.PlayerflagStartPos;
                            //}
                            //else
                            //{
                            //    Target = Managers.LevelManager.instance.PlayerflagStartPos;
                            //}
                            Target = Managers.LevelManager.instance.PlayerflagStartPos;

                            carryingflag = true;

                            if (agent != null)
                            {
                                agent.speed = stats.GetSpeed() / 1.25f;
                                agent.SetDestination(Target.position);
                            }
                           
                            return;
                        }
                    }
                    else
                    {
                        Target.parent = transform;
                        //if (!flaginpos)
                        //{
                        //    Target = Managers.LevelManager.instance.PlayerflagStartPos;
                        //}
                        //else
                        //{
                            Target = Managers.LevelManager.instance.PlayerflagStartPos;
                        //}

                        //Target = Managers.LevelManager.instance.playerflag.transform;

                        carryingflag = true;

                        agent.speed = stats.GetSpeed() / 1.25f;

                        if (agent != null)
                        {
                            agent.SetDestination(Target.position);
                        }
                    }
                    
                   
                }
            }
            else if (stats.enemy && Target.CompareTag("PlayerFalg"))
            {
                if (Vector3.Distance(Target.position, transform.position) < 2)
                {
                    if (Target.parent)
                    {
                        if (Target.parent.GetComponent<Stats.CharacterStats>().GetSpeed() > stats.GetSpeed())
                        {
                            Target.parent.GetComponent<AI.AIMovement>().LoseFlag();
                            Target.parent = transform;
                            //if (!flaginpos)
                            //{
                            //    Target = Managers.LevelManager.instance.EnemyflagStartPos;
                            //}
                            //else
                            //{
                                Target = Managers.LevelManager.instance.EnemyflagStartPos;
                            //}
                            carryingflag = true;

                            agent.speed = stats.GetSpeed() / 1.25f;

                            if (agent != null)
                            {
                                agent.SetDestination(Target.position);
                            }
                           
                            return;
                        }
                        else
                        {

                            Target = FindObjectOfType<Tower>().transform;

                            if (agent != null)
                            {
                                agent.SetDestination(Target.position);
                            }
                        }
                    }
                    else
                    {
                        Target.parent = transform;
                        if (!flaginpos)
                        {
                            Target = Managers.LevelManager.instance.EnemyflagStartPos;
                        }
                        else
                        {
                            Target = Managers.LevelManager.instance.EnemyflagStartPos;
                        }
                        carryingflag = true;

                        if (agent != null)
                        {
                            agent.speed = stats.GetSpeed() / 1.25f;
                            agent.SetDestination(Target.position);
                        }
                    }
                }
            }
            else
            {
                if (Vector3.Distance(Target.position, transform.position) < stats.GetWeapon().Range)
                {
                    if (canAttack)
                    {
                        canAttack = false;
                        Attack();
                        if (anim)
                            anim.SetTrigger("Attack");
                        Invoke("ResetAttack", stats.GetAttackRate());
                    }
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

            if (stats.enemy)
            {
                if (other.CompareTag("Player"))
                {
                    if (other.GetComponentInParent<CharacterStats>().GetComponent<PlayerSideTag>())
                    {
                        Target = other.GetComponentInParent<CharacterStats>().transform;
                        enemies.Add(Target.gameObject);
                        if (agent != null) agent.SetDestination(Target.position);
                        Debug.Log("detected playertroop");
                    }
                }
            }
            else if(stats.playerTroop)
            {
                if (other.CompareTag("Enemy"))
                {
                    if (other.GetComponentInParent<Stats.CharacterStats>().GetComponent<EnemySideTag>())
                    {
                        Target = other.GetComponentInParent<CharacterStats>().transform;
                        enemies.Add(Target.gameObject);
                        if (agent != null) agent.SetDestination(Target.position);
                        Debug.Log("detected enemy");
                    }
                }
            }
        }

        void ResetAttack()
        {
            canAttack = true;
        }

        public void Attack()
        {
            if (Target == null && stats != null) return;

            transform.LookAt(new Vector3(Target.position.x,transform.position.y,Target.position.z));

            var attack = stats.GetWeapon().CreateAttack(stats, Target.GetComponent<CharacterStats>());

            Debug.Log("enemy collided");

            //Get all attackables on the enemy
            var attackables = Target.GetComponentsInParent<IAttackable>();

            //call interfase function on each attackables
            foreach (IAttackable attackable in attackables)
            {
                if(stats != null)
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
