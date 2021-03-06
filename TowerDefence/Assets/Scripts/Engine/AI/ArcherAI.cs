using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Rudrac.TowerDefence.AI
{
    public class ArcherAI : MonoBehaviour
    {
        GameObject[] waypoints;
        NavMeshAgent agent;
        Stats.CharacterStats stats;
        AIArrowFiring fire;
        // Start is called before the first frame update
        void Start()
        {
            fire = GetComponent<AIArrowFiring>();
            agent = GetComponent<NavMeshAgent>();
            stats = GetComponent<Stats.CharacterStats>();
            waypoints = GameObject.FindGameObjectsWithTag("waypoint");
            agent.speed = stats.GetSpeed();

            if (stats.enemy)
            {
                gameObject.AddComponent<EnemySideTag>();
            }
            if (stats.playerTroop)
            {
                gameObject.AddComponent<PlayerSideTag>();
            }

            InvokeRepeating("ChangePosition", Random.Range(0, 10), 20);
            InvokeRepeating("FindTargetAndFIre", Random.Range(0,10), stats.GetAttackRate());
        }

        // Update is called once per frame
        void Update()
        {

        }

        void ChangePosition()
        {
            agent.SetDestination(waypoints[Random.Range(0, waypoints.Length)].transform.position);
        }
        
        void FindTargetAndFIre()
        {
            fire.Target = null;

            if (stats.enemy)
                fire.Target = FindObjectOfType<PlayerSideTag>().transform;
            else
                fire.Target = FindObjectOfType<EnemySideTag>().transform;

            if(fire.Target != null)
                fire.FireArrow();
        }

        

    }
}
