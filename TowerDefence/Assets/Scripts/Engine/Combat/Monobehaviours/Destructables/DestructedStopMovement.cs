using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Rudrac.TowerDefence.Combat
{
    public class DestructedStopMovement : MonoBehaviour, IDestructable
    {
        NavMeshAgent agent;
        public void OnDestruction(GameObject destroyer)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.SetDestination(transform.position);
            agent.enabled = false;
            Destroy(agent);
        }
    }
}
