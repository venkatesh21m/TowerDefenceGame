using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class IceArrow : projectile
    {
        [Space]
        public GameObject IceEffect;
        public float FireTime = 3;
        public float FireAttackRate = 1;
        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }

        public override void OnCollisionEnter(Collision other)
        {
            if (TouchingGround) return;
            _object = other.gameObject;

            if (other.gameObject.name == "Ground" && !TouchingGround)
            {
                GameObject fireeffect = Instantiate(IceEffect, other.GetContact(0).point, IceEffect.transform.rotation);
                fireeffect.transform.parent = transform;
                Destroy(fireeffect, FireTime);

            }

            if (other.collider.CompareTag("Enemy") && !TouchingGround)
            {
                GameObject fireeffect = Instantiate(IceEffect, other.GetContact(0).point, IceEffect.transform.rotation);
                InvokeRepeating("Attack", 1, FireAttackRate);
                fireeffect.transform.parent = transform;
                Destroy(fireeffect, FireTime);
            }

            base.OnCollisionEnter(other);
        }

        public override void Attack()
        {
            base.Attack();
        }
    }
}
