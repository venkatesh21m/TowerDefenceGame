using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Rudrac.TowerDefence.Combat
{
    public class CommetArrow : projectile
    {
        [Space]
        public GameObject CommetEffect;
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
            GameObject comet = Instantiate(CommetEffect, new Vector3(0,25,0) , Quaternion.identity);
            comet.GetComponent<Comet>().attackDef = attackDef;
            comet.GetComponent<Comet>().stats = stats;

            comet.transform.DOMove(other.GetContact(0).point, falltime);
            Destroy(comet, 4);
            Destroy(gameObject, 3);
            base.OnCollisionEnter(other);
        }
    }
}
