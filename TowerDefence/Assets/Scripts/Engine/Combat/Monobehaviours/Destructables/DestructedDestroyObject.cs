using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat 
{
    public class DestructedDestroyObject : MonoBehaviour, IDestructable
    {
        public float DeathAnimationTime = 1.25f;
        public void OnDestruction(GameObject destroyer)
        {
            Destroy(gameObject,DeathAnimationTime);
        }
    }
}