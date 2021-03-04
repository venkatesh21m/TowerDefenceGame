using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class IceWaveArrow : IceArrow
    {
        Vector3 point;
        public float radius;
        public float eachfiredistance;
        public override void Update()
        {
            base.Update();
        }

        public override void OnCollisionEnter(Collision other)
        {
            if (TouchingGround) return;

            _object = other.gameObject;

            point = other.GetContact(0).point;

            StartCoroutine(FireWave());

            base.OnCollisionEnter(other);
        }

        IEnumerator FireWave()
        {
            float x = point.x;
            for (int i = 0; i < 5; i++)
            {
                x += eachfiredistance;
                Destroy(Instantiate(IceEffect, new Vector3(x, point.y, point.z), IceEffect.transform.rotation), FireTime);

                Collider[] hitColliders = Physics.OverlapSphere(new Vector3(x, point.y, point.z), radius);
                foreach (var hitCollider in hitColliders)
                {
                    _object = hitCollider.gameObject;
                    base.Attack();
                }
                
                yield return new WaitForSeconds(0.5f);

            }
        }
    }
}

