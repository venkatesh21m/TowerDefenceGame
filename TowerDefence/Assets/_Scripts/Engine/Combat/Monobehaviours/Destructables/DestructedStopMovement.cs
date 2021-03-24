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
            if (agent != null)
            {
                agent.velocity = Vector3.zero;
                agent.enabled = false;
                Destroy(agent);
            }
        }
    }
}
