using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rudrac.TowerDefence.Combat
{
    public class meteorArrow : projectile
    {
        [Space]
        public GameObject MeteorEffect;
        public float falltime = .2f;

        public override void Update()
        {
            base.Update();
        }

        public override void OnCollisionEnter(Collision other)
        {
            if (TouchingGround) return;
            _object = other.gameObject;

            GetComponentInChildren<MeshRenderer>().enabled = false;
            GameObject comet = Instantiate(MeteorEffect, new Vector3(0, 25, 0), Quaternion.identity);
            comet.GetComponent<Comet>().attackDef = attackDef;
            comet.GetComponent<Comet>().stats = stats;

            comet.transform.DOMove(other.GetContact(0).point, falltime);
           
            Destroy(comet, 4);
            Destroy(gameObject, 2);
            base.OnCollisionEnter(other);

        }
    }
}
        