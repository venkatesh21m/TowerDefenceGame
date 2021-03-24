using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class DestructableRagdoll : MonoBehaviour, IDestructable
    {
        public void OnDestruction(GameObject destroyer)
        {
            var Rigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach (var item in Rigidbodies)
            {
                item.useGravity = true;
                item.isKinematic = false;

                item.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
        }
    }
}
