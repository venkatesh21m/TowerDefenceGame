using UnityEngine;
using System.Collections;


namespace Rudrac.TowerDefence
{
    public class projectile : MonoBehaviour
    {

        // cache
        public Rigidbody rigid;
        private bool bTouchingGround;

        // Update is called once per frame
        void Update()
        {
            if (!bTouchingGround && rigid.velocity != Vector3.zero)
            {
                // update the rotation of the projectile during trajectory motion
                transform.rotation = Quaternion.LookRotation(rigid.velocity);
            }
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Ground")
            {
                bTouchingGround = true;
                rigid.isKinematic = true;
                Destroy(gameObject, 3);
            }
        }

    }
}