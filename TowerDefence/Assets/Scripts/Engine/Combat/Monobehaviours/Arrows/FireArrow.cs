using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class FireArrow : projectile
    {
        [Space]
        public GameObject FireEffect;
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
                GameObject fireeffect = Instantiate(FireEffect, other.GetContact(0).point, FireEffect.transform.rotation);
                fireeffect.transform.parent = transform;
                Destroy(fireeffect, FireTime);

            }

            if (other.collider.CompareTag("Enemy") && !TouchingGround)
            {
                GameObject fireeffect = Instantiate(FireEffect, other.GetContact(0).point, FireEffect.transform.rotation);
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
