using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Rudrac.TowerDefence.Combat
{
    public class BombArrow : projectile
    {

        [Space]
        public GameObject BombEffect;
        public float radius;
        public override void Update()
        {
            base.Update();
        }

        public override void OnCollisionEnter(Collision other)
        {
            if (TouchingGround) return;
            _object = other.gameObject;

            GetComponentInChildren<MeshRenderer>().enabled = false;
            GameObject fireeffect = Instantiate(BombEffect, other.GetContact(0).point, Quaternion.identity);
            Destroy(fireeffect, 1);
           // TouchingGround = true;

            base.OnCollisionEnter(other);


            Collider[] hitColliders = Physics.OverlapSphere(other.GetContact(0).point, radius);
            foreach (var hitCollider in hitColliders)
            {
                _object = hitCollider.gameObject;
                base.Attack();
            }


        }

    }
}
